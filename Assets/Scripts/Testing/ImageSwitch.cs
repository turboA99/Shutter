using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Testing
{
    public class ImageSwitch : MonoBehaviour
    {
        [SerializeField]
        InputAction inputActionPrevious;
        [SerializeField]
        InputAction inputActionNext;
        [SerializeField]
        List<Sprite> images;

        SpriteRenderer _image;
        int _currentImageIndex = 0;

        void Awake()
        {
            _image = GetComponent<SpriteRenderer>();
            _image.sprite = images[_currentImageIndex];
            if (_image == null) throw new UnityException("Image not found");
            inputActionNext.Enable();
            inputActionNext.started += context =>
            {
                if (context.ReadValueAsButton())
                {
                    _currentImageIndex++;
                    if (_currentImageIndex >= images.Count)  _currentImageIndex = 0;
                    _image.sprite = images[_currentImageIndex];
                }
            };
            inputActionPrevious.Enable();
            inputActionPrevious.started += context =>
            {
                if (context.ReadValueAsButton())
                {
                    _currentImageIndex++;
                    if (_currentImageIndex >= images.Count)  _currentImageIndex = 0;
                    _image.sprite = images[_currentImageIndex];
                }
            };
        }
    }
}
