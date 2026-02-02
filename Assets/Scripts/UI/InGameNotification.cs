using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class InGameNotification : MonoBehaviour
    {
        [SerializeField] List<Graphic> visualElements;
        [SerializeField] TextMeshProUGUI messageText;
        [SerializeField] float fadeDuration = .5f;
        [SerializeField] float showDuration = 2f;

        readonly Dictionary<Graphic, Color> _rememberColor = new();
        SimpleAnimation _fadeIn;
        SimpleAnimation _fadeOut;
        Action _onDestroyed;

        void Awake()
        {
            _fadeIn = new SimpleAnimation(fadeDuration,
                progress =>
                {
                    visualElements.ForEach(visualElement =>
                    {
                        visualElement.color = Color.Lerp(Color.clear, _rememberColor[visualElement], progress);
                    });
                },
                () => StartCoroutine(WaitAndFadeOut()));
            _fadeOut = new SimpleAnimation(fadeDuration,
                progress => visualElements.ForEach(visualElement =>
                {
                    visualElement.color = Color.Lerp(_rememberColor[visualElement], Color.clear, progress);
                }),
                () =>
                {
                    _onDestroyed?.Invoke();
                    Destroy(gameObject);
                });
        }

        IEnumerator WaitAndFadeOut()
        {
            yield return new WaitForSeconds(showDuration);
            _fadeOut.Play();
        }


        void OnEnable()
        {
            _fadeIn.Play();
        }

        void OnDisable()
        {
            Destroy(gameObject);
        }

        void Update()
        {
            if (_fadeIn.IsPlaying)
            {
                _fadeIn.Update();
            }
            if (_fadeOut.IsPlaying)
            {
                _fadeOut.Update();
            }
        }

        public void Initialize(Notification notification, Action onDestroyed)
        {
            _onDestroyed = onDestroyed;
            foreach (var visualElement in visualElements)
            {
                _rememberColor.Add(visualElement, visualElement.color);
            }
            showDuration = notification.NotificationDuration;
            messageText.text = notification.Message;
        }
    }
}
