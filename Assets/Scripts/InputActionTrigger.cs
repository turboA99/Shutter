using System;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputActionTrigger : MonoBehaviour
{
    [Flags] enum ActionType
    {
        None,
        Started,
        Cancelled,
        Performed
    }

    [SerializeField] InputActionProperty trigger;

    [SerializeField]
    ActionType actionType = ActionType.Started | ActionType.Cancelled | ActionType.Performed;

    [SerializeField] UnityEvent eventToTrigger;

    void Awake()
    {
    }

    void OnTriggered(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            eventToTrigger?.Invoke();
        }
    }

    void OnEnable()
    {
        if (actionType.HasFlag(ActionType.Started))
        {
            trigger.action.started += OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Cancelled))
        {
            trigger.action.canceled += OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Performed))
        {
            trigger.action.performed += OnTriggered;
        }
    }

    void OnDisable()
    {
        if (actionType.HasFlag(ActionType.Started))
        {
            trigger.action.started -= OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Cancelled))
        {
            trigger.action.canceled -= OnTriggered;
        }
        if (actionType.HasFlag(ActionType.Performed))
        {
            trigger.action.performed -= OnTriggered;
        }
    }
}
