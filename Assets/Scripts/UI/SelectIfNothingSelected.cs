using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectIfNothingSelected : MonoBehaviour
{
    [SerializeField] GameObject target;
    
    EventSystem _eventSystem;
    InputAction _navigateAction;

    void Awake()
    {
        _eventSystem = EventSystem.current;
        if (!_eventSystem) throw new UnityException("No EventSystem found");
        
        _navigateAction = InputSystem.actions.FindAction("UI/Navigate");
        _navigateAction.started += OnNavigate;
        _navigateAction.performed += OnNavigate;
        _navigateAction.canceled += OnNavigate;
        
    }

    void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame() && !_eventSystem.currentSelectedGameObject)
        {
            _eventSystem.SetSelectedGameObject(target);
        }
    }
}
