using System;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionTrigger : MonoBehaviour
{
    [Flags] enum ActionType
    {
        Started,
        Cancelled,
        Performed
    }

    [SerializeField]
    InputActionReference inputActionReference;

    [SerializeField]
    ActionType actionType = ActionType.Started | ActionType.Cancelled | ActionType.Performed;

    [SerializeField]
    UnityEvent eventToTrigger;

    InputAction _inputAction;

    void Awake()
    {
        _inputAction = inputActionReference.action;
    }

    void OnTriggered(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            AwarenessManager.instance.IncreaseAwareness(NoiseMade.Medium);
            eventToTrigger?.Invoke();
        }
    }

    void OnEnable()
    {
        if (actionType.HasFlag(ActionType.Started))
        {
            _inputAction.started += OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Cancelled))
        {
            _inputAction.canceled += OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Performed))
        {
            _inputAction.performed += OnTriggered;
        }
    }

    void OnDisable()
    {
        if (actionType.HasFlag(ActionType.Started))
        {
            _inputAction.started -= OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Cancelled))
        {
            _inputAction.canceled -= OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Performed))
        {
            _inputAction.performed -= OnTriggered;
        }
    }
}
