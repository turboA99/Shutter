using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class AlertIndicator : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)]
        float levelLowThreshold = .1f;

        [SerializeField] [Range(0f, 1f)]
        float levelMediumLowThreshold = .3f;

        [SerializeField] [Range(0f, 1f)]
        float levelMediumThreshold = .5f;

        [SerializeField] [Range(0f, 1f)]
        float levelTooMuchThreshold = .7f;

        [SerializeField] [Range(0f, 1f)]
        float levelDangerThreshold = .95f;

        [SerializeField]
        Sprite spriteLevelLow;

        [SerializeField]
        Sprite spriteLevelMediumLow;

        [SerializeField]
        Sprite spriteLevelMedium;

        [SerializeField]
        Sprite spriteLevelTooMuch;

        [SerializeField]
        float maxLoudness = 100;
        
        float _currentLoudness = 0;

        Image _image;

        void Awake()
        {
            _image = GetComponent<Image>();
        }

        void UpdateSprite()
        {
            var progress = _currentLoudness / maxLoudness;
            if (progress < levelLowThreshold)
            {
                _image.color = new Color(1, 1, 1, 0);
            }
            else
            {
                _image.color = new Color(1, 1, 1, 1);
            }
            if (progress >= levelLowThreshold && progress < levelMediumLowThreshold)
            {
                _image.sprite = spriteLevelLow;
            }
            else if (progress >= levelMediumLowThreshold && progress < levelMediumThreshold)
            {
                _image.sprite = spriteLevelMediumLow;
            }
            else if (progress >= levelMediumThreshold && progress < levelTooMuchThreshold)
            {
                _image.sprite = spriteLevelMedium;
            }
            else if (progress >= levelTooMuchThreshold)
            {
                _image.sprite = spriteLevelTooMuch;
            }
            if (progress >= levelDangerThreshold)
            {
                _image.color = new Color(1, 0, 0, 0);
            }
        }

        public void UpdateLoudness(float loudness)
        {
            _currentLoudness = loudness;
            UpdateSprite();
        }
    }
}
