using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Effects
{
    public class FadeInOutOnEnable : MonoBehaviour
    {
        [SerializeField] float fadeDuration = .2f;

        [Tooltip("The graphics to fade in and out")]
        [SerializeField] List<Graphic> images;

        SimpleAnimation _animationFadeIn;
        SimpleAnimation _animationFadeOut;


        readonly Dictionary<object, Color> _rememberedColors = new();

        void Awake()
        {
            images.ForEach(image => _rememberedColors.Add(image, image.color));

            _animationFadeIn = new SimpleAnimation(fadeDuration,
                progress =>
                {
                    images.ForEach(image =>
                    {
                        Color color = _rememberedColors[image];
                        Color transparent = color;
                        transparent.a = 0;
                        image.color = Color.Lerp(transparent, color, progress);
                    });
                });
            _animationFadeOut = new SimpleAnimation(fadeDuration,
                progress =>
                {
                    images.ForEach(image =>
                    {
                        Color color = _rememberedColors[image];
                        Color transparent = color;
                        transparent.a = 0;
                        image.color = Color.Lerp(color, transparent, progress);
                    });
                });
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
    }
}
