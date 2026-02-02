using System;
using Effects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class AlertIndicator : MonoBehaviour
    {
        enum State
        {
            None,
            Low,
            MediumLow,
            Medium,
            TooLoud,
            Danger
        }
        [FormerlySerializedAs("shakeLevelTooMuch")]
        [SerializeField]
        Shake shakeLevelTooLoud;

        [SerializeField]
        Shake shakeLevelDanger;

        [SerializeField] [Range(0f, 1f)]
        float levelLowThreshold = .1f;

        [SerializeField] [Range(0f, 1f)]
        float levelMediumLowThreshold = .3f;

        [SerializeField] [Range(0f, 1f)]
        float levelMediumThreshold = .5f;

        [FormerlySerializedAs("levelTooMuchThreshold")]
        [SerializeField] [Range(0f, 1f)]
        float levelTooLoudThreshold = .7f;

        [SerializeField] [Range(0f, 1f)]
        float levelDangerThreshold = .95f;

        [SerializeField]
        Sprite spriteLevelLow;

        [SerializeField]
        Sprite spriteLevelMediumLow;

        [SerializeField]
        Sprite spriteLevelMedium;

        [FormerlySerializedAs("spriteLevelTooMuch")]
        [SerializeField]
        Sprite spriteLevelTooLoud;

        [SerializeField]
        float maxLoudness = 100;

        float _currentLoudness;

        Image _image;
        State _currentState = State.None;

        void Awake()
        {
            _image = GetComponent<Image>();
        }

        void Start()
        {
            UpdateLoudness(_currentLoudness);
        }

        void UpdateSprite()
        {
            var progress = _currentLoudness / maxLoudness;
            if (!_image) _image = GetComponent<Image>();
            //Image graphic
            switch (_currentState)
            {
                case State.None:
                    _image.enabled = false;
                    break;
                default:
                    _image.enabled = true;
                    break;
            }
            //Sprite
            switch (_currentState)
            {

                case State.None:
                    break;
                case State.Low:
                    _image.sprite = spriteLevelLow;
                    break;
                case State.MediumLow:
                    _image.sprite = spriteLevelMediumLow;
                    break;
                case State.Medium:
                    _image.sprite = spriteLevelMedium;
                    break;
                case State.TooLoud:
                    _image.sprite = spriteLevelTooLoud;
                    break;
                case State.Danger:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            //Color
            switch (_currentState)
            {
                case State.Danger:
                    _image.color = Color.red;
                    break;
                default:
                    _image.color = Color.white;
                    break;
            }
            //Shake
            switch (_currentState)
            {
                case State.TooLoud:
                    shakeLevelDanger.enabled = false;
                    shakeLevelTooLoud.enabled = true;
                    break;
                case State.Danger:
                    shakeLevelTooLoud.enabled = false;
                    shakeLevelDanger.enabled = true;
                    break;
                default:
                    shakeLevelTooLoud.enabled = false;
                    shakeLevelDanger.enabled = false;
                    break;
            }
        }

        public void UpdateLoudness(float loudness)
        {
            _currentLoudness = loudness;
            var progress = _currentLoudness / maxLoudness;
            if (progress < levelLowThreshold)
            {
                _currentState = State.None;
            }
            else if (progress >= levelLowThreshold && progress < levelMediumLowThreshold)
            {
                _currentState = State.Low;
            }
            else if (progress >= levelMediumLowThreshold && progress < levelMediumThreshold)
            {
                _currentState = State.MediumLow;
            }
            else if (progress >= levelMediumThreshold && progress < levelTooLoudThreshold)
            {
                _currentState = State.Medium;
            }
            else if (progress >= levelTooLoudThreshold &&  progress < levelDangerThreshold)
            {
                _currentState = State.TooLoud;
            } 
            else if (progress >= levelDangerThreshold)
            {
                _currentState = State.Danger;
            }
            
            UpdateSprite();
        }
    }
}
