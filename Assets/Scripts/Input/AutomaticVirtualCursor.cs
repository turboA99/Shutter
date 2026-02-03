using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AutomaticVirtualCursor : MonoBehaviour
{
    [Tooltip("The graphic that represents the software cursor. This is hidden if a hardware cursor (see 'Cursor Mode') is used.")]
    [SerializeField]
    Graphic cursorGraphic;

    [Tooltip("The transform for the software cursor. Will only be set if a software cursor is used (see 'Cursor Mode'). Moving the cursor "
             + "updates the anchored position of the transform.")]
    [SerializeField]
    RectTransform cursorTransform;

    [Header("Motion")]
    [Tooltip("Speed in pixels per second with which to move the cursor. Scaled by the input from 'Stick Action'.")]
    [SerializeField]
    float cursorSpeed = 400;

    [Tooltip("Scale factor to apply to 'Scroll Wheel Action' when setting the mouse 'scrollWheel' control.")]
    [SerializeField]
    float scrollSpeed = 45;

    [FormerlySerializedAs("m_StickAction")]
    [Space(10)]
    [Tooltip("Vector2 action that moves the cursor left/right (X) and up/down (Y) on screen.")]
    [SerializeField]
    InputActionProperty stickAction;

    [Tooltip("Button action that triggers a left-click on the mouse.")]
    [SerializeField]
    InputActionProperty pressAction;

    [Tooltip("Vector2 action that feeds into the mouse 'scrollWheel' action (scaled by 'Scroll Speed').")]
    [SerializeField]
    InputActionProperty scrollAction;

    Action _afterInputUpdateDelegate;
    Action _beforeInputUpdateDelegate;

    IDisposable _disposable;
    Vector2 _lastStickValue;
    Pointer _nativePointer;
    Action<InputAction.CallbackContext> _pressActionTriggeredDelegate;
    Action<InputAction.CallbackContext> _stickActionTriggeredDelegate;
    bool _wasPressed;
    bool _doEnable = true;

    public RectTransform CursorTransform
    {
        get => cursorTransform;
        set => cursorTransform = value;
    }

    public float CursorSpeed
    {
        get => cursorSpeed;
        set => cursorSpeed = value;
    }

    public Graphic CursorGraphic
    {
        get => cursorGraphic;
        set => cursorGraphic = value;
    }

    public float ScrollSpeed
    {
        get => scrollSpeed;
        set => scrollSpeed = value;
    }

    public VirtualCursor VirtualCursor { get; private set; }

    public InputActionProperty StickAction
    {
        get => stickAction;
        set => SetAction(ref stickAction, value);
    }

    public InputActionProperty ClickAction
    {
        get => pressAction;
        set
        {
            if (_pressActionTriggeredDelegate != null)
                SetActionCallback(pressAction, _pressActionTriggeredDelegate, false);
            SetAction(ref pressAction, value);
            if (_pressActionTriggeredDelegate != null)
                SetActionCallback(pressAction, _pressActionTriggeredDelegate);
        }
    }

    public InputActionProperty ScrollWheelAction
    {
        get => scrollAction;
        set => SetAction(ref scrollAction, value);
    }

    public void SetEnabled(bool state)
    {
        _doEnable = state;
        enabled = state;
        if (!state) Cursor.visible = true;
    }

    void Awake()
    {
        InputSystem.RegisterLayout<VirtualCursor>("VirtualCursor");
    }

    protected void OnEnable()
    {
        if (!_doEnable) _doEnable = true;
        // Add mouse device.
        if (VirtualCursor == null)
        {
            VirtualCursor = (VirtualCursor)InputSystem.AddDevice("VirtualCursor");
        }
        else if (!VirtualCursor.added)
        {
            InputSystem.AddDevice(VirtualCursor);
            VirtualCursor.ConfineToViewport = true;
        }

        _nativePointer = InputSystem.GetDevice<Pointer>();
        if (_nativePointer is not { synthetic: false }) _nativePointer = null;

        // Set initial cursor position.
        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            VirtualCursor.MoveTo(position);
        }

        if (_nativePointer != null)
            VirtualCursor.MoveTo(_nativePointer.position.ReadValue());

        // Hook into input update.
        if (_afterInputUpdateDelegate == null)
            _afterInputUpdateDelegate = OnAfterInputUpdate;
        InputSystem.onAfterUpdate += _afterInputUpdateDelegate;

        if (_beforeInputUpdateDelegate == null)
            _beforeInputUpdateDelegate = OnBeforeInputUpdate;
        InputSystem.onBeforeUpdate += _beforeInputUpdateDelegate;

        ControlSchemeChangeObserver.OnControlSchemeChangedEvent += ControlSchemeChanged;
        ControlSchemeChanged(ControlSchemeChangeObserver.GetCurrentControlScheme());

        // Hook into actions.
        if (_pressActionTriggeredDelegate == null)
            _pressActionTriggeredDelegate = OnPressActionTriggered;
        SetActionCallback(pressAction, _pressActionTriggeredDelegate);

        if (_stickActionTriggeredDelegate == null)
            _stickActionTriggeredDelegate = OnStickActionTriggered;
        SetActionCallback(stickAction, _stickActionTriggeredDelegate);

        // Enable actions.
        stickAction.action?.Enable();
        pressAction.action?.Enable();
        scrollAction.action?.Enable();
    }

    protected void OnDisable()
    {
        // Remove mouse device.
        if (VirtualCursor != null && VirtualCursor.added)
            InputSystem.RemoveDevice(VirtualCursor);

        // Remove ourselves from input update.
        if (_afterInputUpdateDelegate != null)
            InputSystem.onAfterUpdate -= _afterInputUpdateDelegate;

        if (_beforeInputUpdateDelegate != null)
            InputSystem.onBeforeUpdate -= OnBeforeInputUpdate;

        // Disable actions.
        stickAction.action?.Disable();
        pressAction.action?.Disable();
        scrollAction.action?.Disable();

        // Unhock from actions.
        if (_pressActionTriggeredDelegate != null)
        {
            SetActionCallback(pressAction, _pressActionTriggeredDelegate, false);
        }
        if (_stickActionTriggeredDelegate != null)
        {
            SetActionCallback(stickAction, _stickActionTriggeredDelegate, false);
        }
        
        CursorTransform.gameObject.SetActive(false);
        _lastStickValue = default;
    }



    void ControlSchemeChanged(string controlScheme)
    {
        if (!enabled) return;
        switch (controlScheme)
        {
            case "Keyboard&Mouse":
                CursorTransform.gameObject.SetActive(false);
                Cursor.visible = true;
                enabled = false;
                break;
            case "Xbox Controller":
            case "Playstation Controller":
            case "Gamepad":
                CursorTransform.gameObject.SetActive(true);
                Cursor.visible = false;
                enabled = true;
                break;
            default:
                throw new Exception("Unknown control scheme " + controlScheme);
        }
    }

    void UpdateMotion()
    {
        if (VirtualCursor == null)
            return;


        // Update software cursor transform, if any.
        cursorTransform.anchoredPosition = VirtualCursor.position.value;


        // Update scroll wheel.
        InputAction scrollAction = this.scrollAction.action;
        if (scrollAction != null)
        {
            Vector2 scrollValue = scrollAction.ReadValue<Vector2>();
            scrollValue.x *= scrollSpeed;
            scrollValue.y *= scrollSpeed;

            InputSystem.QueueDeltaStateEvent(VirtualCursor.scroll, scrollValue);
        }
    }

    void OnPressActionTriggered(InputAction.CallbackContext context)
    {
        if (VirtualCursor == null)
            return;

        _wasPressed = context.control.IsPressed();

    }


    void OnStickActionTriggered(InputAction.CallbackContext context)
    {
        Vector2 stickValue = context.ReadValue<Vector2>();

        if (Mathf.Approximately(0, stickValue.x) && Mathf.Approximately(0, stickValue.y))
        {
            _lastStickValue = default;
        }
        else
        {
            _lastStickValue = stickValue;
        }
    }

    static void SetActionCallback(InputActionProperty field, Action<InputAction.CallbackContext> callback, bool install = true)
    {
        InputAction action = field.action;
        if (action == null)
            return;

        // We don't need the performed callback as our mouse buttons are binary and thus
        // we only care about started (1) and canceled (0).

        if (install)
        {
            action.started += callback;
            action.performed += callback;
            action.canceled += callback;
        }
        else
        {
            action.started -= callback;
            action.performed -= callback;
            action.canceled -= callback;
        }
    }

    static void SetAction(ref InputActionProperty field, InputActionProperty value)
    {
        InputActionProperty oldValue = field;
        field = value;

        if (oldValue.reference == null)
        {
            InputAction oldAction = oldValue.action;
            if (oldAction != null && oldAction.enabled)
            {
                oldAction.Disable();
                if (value.reference == null)
                    value.action?.Enable();
            }
        }
    }


    void OnAfterInputUpdate()
    {
        UpdateMotion();
    }

    void OnBeforeInputUpdate()
    {
        var delta = new Vector2(cursorSpeed * _lastStickValue.x * Time.deltaTime, cursorSpeed * _lastStickValue.y * Time.deltaTime);
        Vector2 newPosition = VirtualCursor.position.value + delta;
        if (delta.sqrMagnitude > 0.01f)
            VirtualCursor.MoveTo(newPosition);

        if (VirtualCursor.press.isPressed != _wasPressed) VirtualCursor.SetPressed(_wasPressed);
    }
}
