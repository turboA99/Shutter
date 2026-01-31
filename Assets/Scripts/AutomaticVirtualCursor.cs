using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Utilities;



public class AutomaticVirtualCursor : VirtualMouseInput
{
    PlayerInput _playerInput;
    string _rememberControlScheme;

    IDisposable _disposable;

    void Awake()
    {
        _playerInput = FindAnyObjectByType<PlayerInput>();
        if (!_playerInput) throw new Exception("No PlayerInput found in scene");
        _rememberControlScheme = _playerInput.currentControlScheme;
        
        _disposable = InputSystem.onEvent.Call(OnEvent);
    }

    void OnEvent(InputEventPtr inputEventPtr)
    {
        if (_playerInput.currentControlScheme is "XboxController" or "PlaystationController")
        {
            
        }
        if (_playerInput.currentControlScheme == _rememberControlScheme) return;
        
        _rememberControlScheme = _playerInput.currentControlScheme;
        
        CheckControlScheme();
    }

    void CheckControlScheme()
    {
        switch (_playerInput.currentControlScheme)
        {
            case "Keyboard&Mouse":
                cursorTransform.gameObject.SetActive(false);
                enabled = false;
                break;
            case "XboxController":
                cursorTransform.gameObject.SetActive(true);
                enabled = true;
                break;
            case "PlaystationController":
                cursorTransform.gameObject.SetActive(true);
                enabled = true;
                break;
        }
    }

    void OnDestroy()
    {
        _disposable.Dispose();
    }
}
