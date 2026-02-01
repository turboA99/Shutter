using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionTrigger : MonoBehaviour
{
    [SerializeField]
    InputActionReference inputActionReference;

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
        _inputAction.started += OnTriggered;
    }

    void OnDisable()
    {
        _inputAction.started -= OnTriggered;
    }
}
