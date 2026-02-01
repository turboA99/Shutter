using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class FadeInOutOnEnable : MonoBehaviour
    {
        [SerializeField]float fadeDuration = .2f;
    
        [Tooltip("The images to fade in and out")]
        [SerializeField] List<Graphic> images;
    
    
        Dictionary<object, Color> _rememberedColors = new();
        SimpleAnimation _animationFadeIn;
        SimpleAnimation _animationFadeOut;

        void Awake()
        {
            images.ForEach(image => _rememberedColors.Add(image, image.color));
        
            _animationFadeIn = new SimpleAnimation(fadeDuration,
                progress =>
                {
                    images.ForEach(image =>
                    {
                        var color = _rememberedColors[image];
                        var transparent = color;
                        transparent.a = 0;
                        image.color = Color.Lerp(transparent, color, progress);
                    });
                });
            _animationFadeOut = new SimpleAnimation(fadeDuration,
                progress =>
                {
                    images.ForEach(image =>
                    {
                        var color = _rememberedColors[image];
                        var transparent = color;
                        transparent.a = 0;
                        image.color = Color.Lerp(color, transparent, progress);
                    });
                });
        }

        void OnEnable()
        {
            FadeIn();
        }

        void OnDisable()
        {
            _animationFadeOut.OnAnimationFinished -= Disable;
        }

        public void FadeIn()
        {
            _animationFadeOut.PauseAndReset();
            _animationFadeIn.Reset();
            _animationFadeIn.Play();
        }

        public void FadeOut()
        {
            _animationFadeIn.PauseAndReset();
            _animationFadeOut.Reset();
            _animationFadeOut.Play();
        }

        void Disable()
        {
            gameObject.SetActive(false);
        }

        public void FadeOutAndDisable()
        {
            FadeOut();
            _animationFadeOut.OnAnimationFinished += Disable;
        }

        void Update()
        {
            if (_animationFadeIn.IsPlaying)
            {
                _animationFadeIn.Update();
            }
            if (_animationFadeOut.IsPlaying)
            {
                _animationFadeOut.Update();
            }
        }
    }
}
