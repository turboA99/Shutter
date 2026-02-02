using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace UI.Input
{
    [RequireComponent(typeof(Image))]
    public class InputGraphicChanger : MonoBehaviour
    {
        [SerializeField] Sprite keyboardMouseSprite;
        [SerializeField] Sprite xboxSprite;
        [SerializeField] Sprite playstationSprite;
        [SerializeField] Sprite gamepadSprite;
    
        PlayerInput _playerInput;
        Image _image;
        string _rememberControlScheme;

        IDisposable _disposable;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Awake()
        {
            _image = GetComponent<Image>();
            _playerInput = FindAnyObjectByType<PlayerInput>();
            if (!_playerInput) throw new Exception("No PlayerInput found in scene");
            _rememberControlScheme = _playerInput.currentControlScheme;
        }

        void OnEvent(InputEventPtr _)
        {
            if (_playerInput.currentControlScheme == _rememberControlScheme) return;
        
            _rememberControlScheme = _playerInput.currentControlScheme;
            SwitchGraphic();
        }

        void SwitchGraphic()
        {
            switch (_playerInput.currentControlScheme)
            {
                case "Keyboard&Mouse":
                    _image.sprite = keyboardMouseSprite;
                    break;
                case "Xbox Controller":
                    _image.sprite = xboxSprite;
                    break;
                case "Playstation Controller":
                    _image.sprite = playstationSprite;
                    break;
                case "Gamepad":
                    _image.sprite = gamepadSprite;
                    break;
            }
        }

        void OnEnable()
        {
            SwitchGraphic();
            _disposable = InputSystem.onEvent.Call(OnEvent);
        }
    
        void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}
