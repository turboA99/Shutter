using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionTrigger : MonoBehaviour
{
    enum ActionType
    {
        Started,
        Cancelled,
        Performed
    }
    [SerializeField]
    InputActionReference inputActionReference;
    [SerializeField]
    ActionType actionType = ActionType.Started;
    
    [SerializeField]
    UnityEvent eventToTrigger;

    InputAction _inputAction;

    void Awake()
    {
        _inputAction = inputActionReference.action;
    }

    void OnTriggered(InputAction.CallbackContext context)
    {
        eventToTrigger?.Invoke();
    }

    void OnEnable()
    {
        switch (actionType)
        {
            case ActionType.Started:
                _inputAction.started += OnTriggered;
                break;
            case ActionType.Cancelled:
                _inputAction.canceled += OnTriggered;
                break;
            case ActionType.Performed:
                _inputAction.performed += OnTriggered;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnDisable()
    {
        switch (actionType)
        {
            case ActionType.Started:
                _inputAction.started -= OnTriggered;
                break;
            case ActionType.Cancelled:
                _inputAction.canceled -= OnTriggered;
                break;
            case ActionType.Performed:
                _inputAction.performed -= OnTriggered;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
