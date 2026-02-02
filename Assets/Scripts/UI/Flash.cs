using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using Utils;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class Flash : MonoBehaviour
    {
        [Header("Flash")]
        [SerializeField] private float cooldown;
        [Header("Animation")]
        [SerializeField]
        float flashInDuration = .1f;
        [SerializeField]
        float flashOutDuration = 1f;
        [SerializeField]
        float fadeOutDuration = 5f;
        [SerializeField]
        Color darknessColor = Color.black;
        [SerializeField]
        Color semiDarkColor = new(0f, 0f, 0f, .4f);
        
        public UnityEvent OnFlash;
    
        SimpleAnimation _flashIn;
        SimpleAnimation _flashOut;
        SimpleAnimation _fadeOut;
        Image _image;
        
        bool _canFlash = true;

        void Awake()
        {
            _image  =  GetComponent<Image>();
            _flashIn = new SimpleAnimation(
                flashInDuration,
                progress => _image.color = Color.Lerp(darknessColor, Color.white, progress),
                () =>
                {
                    OnFlash?.Invoke();
                    _flashIn.PauseAndReset();
                    _flashOut.Play();
                });
            _flashOut = new SimpleAnimation(
                flashOutDuration,
                progress => _image.color = Color.Lerp(Color.white, semiDarkColor, progress),
                () =>
                {
                    _flashOut.PauseAndReset();
                    _fadeOut.Play();
                });
            _fadeOut = new SimpleAnimation(
                fadeOutDuration,
                progress => _image.color = Color.Lerp(semiDarkColor, darknessColor, Easing.OutCirc(progress)),
                () =>
                {
                    _fadeOut.Reset();
                });
        }

        void OnEnable()
        {
            _image.color = darknessColor;
        }

        void Update()
        {
            if (_flashIn.IsPlaying) _flashIn.Update();
            if (_flashOut.IsPlaying) _flashOut.Update();
            if (_fadeOut.IsPlaying) _fadeOut.Update();
        }

        public void DoFlash()
        {
            if (!_canFlash) return;
            _flashIn.PauseAndReset();
            _flashOut.PauseAndReset();
            _fadeOut.PauseAndReset();
            _flashIn.Play();
            _canFlash = false;
            StartCoroutine(Cooldown());
        }

        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldown);
            _canFlash = true;
        }
    }
}
