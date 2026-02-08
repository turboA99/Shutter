using System;
using Input;
using UnityEngine;
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

        Image _image;

        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Awake()
        {
            _image = GetComponent<Image>();
        }

        void OnEnable()
        {
            ControlSchemeChangeObserver.OnControlSchemeChangedEvent += OnControlSchemeChanged;
            OnControlSchemeChanged(ControlSchemeChangeObserver.GetCurrentControlScheme);
        }

        void OnDisable()
        {
            ControlSchemeChangeObserver.OnControlSchemeChangedEvent -= OnControlSchemeChanged;
        }

        void OnControlSchemeChanged(string controlScheme)
        {
            switch (controlScheme)
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
    }
}
