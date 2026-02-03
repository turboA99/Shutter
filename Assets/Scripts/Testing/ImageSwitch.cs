using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        int _currentImageIndex;

        SpriteRenderer _image;

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
                    if (_currentImageIndex >= images.Count) _currentImageIndex = 0;
                    _image.sprite = images[_currentImageIndex];
                }
            };
            inputActionPrevious.Enable();
            inputActionPrevious.started += context =>
            {
                if (context.ReadValueAsButton())
                {
                    _currentImageIndex++;
                    if (_currentImageIndex >= images.Count) _currentImageIndex = 0;
                    _image.sprite = images[_currentImageIndex];
                }
            };
        }
    }
}
