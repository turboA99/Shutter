using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Effects
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextType : MonoBehaviour
    {
        public enum TextAppearType
        {
            Letter,
            Word,
            Line,
        }

        [Header("Text Animation")]
        [SerializeField] TextAppearType textAppearType = TextAppearType.Letter;

        [SerializeField] [Tooltip("The time fir one character to be put on screen")]
        float characterTypeTime = .25f;

        [SerializeField] [Tooltip("The time fir one character to be put on screen")]
        float wordTypeTime = 1.5f;

        [SerializeField] [Tooltip("The time fir one character to be put on screen")]
        float lineTypeTime = 4f;

        [Tooltip("Called when the text fully appeared")]
        [Header("Events")]
        public UnityEvent OnAnimationFinished;

        [Header("Sound")]
        [SerializeField] [Tooltip("The sound played typing")]
        AudioClip typingSound;

        [SerializeField] [Tooltip("The frequency of typing sound being played")]
        int playFrequency = 2;

        SimpleAnimation _animation;
        float _animationDuration;
        AudioSource _audioSource;
        int _rememberFrequencyIndex;
        TMP_TextInfo _rememberInfo;


        string _text;

        TextMeshProUGUI _tmpText;

        float _characterCount => _rememberInfo.characterCount;

        float _wordCount => _rememberInfo.wordCount;

        float _lineCount => _rememberInfo.lineCount;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _tmpText = GetComponent<TextMeshProUGUI>();
            _text = _tmpText.text;
            _rememberInfo = _tmpText.GetTextInfo(_text);
            _audioSource = GetComponentInParent<AudioSource>();
            if (!_audioSource && typingSound) throw new UnityException("AudioSource not found to player the typing sound");
            switch (textAppearType)
            {
                case TextAppearType.Letter:
                    _tmpText.maxVisibleCharacters = 0;
                    _animationDuration = _characterCount * characterTypeTime;
                    _animation = new SimpleAnimation(_animationDuration,
                        progress =>
                        {
                            var lastCharIndex = (int)(progress * _characterCount);
                            if (typingSound && _tmpText.maxVisibleCharacters >= playFrequency + _rememberFrequencyIndex)
                            {
                                _rememberFrequencyIndex = lastCharIndex;
                                _audioSource.PlayOneShot(typingSound);
                            }
                            _tmpText.maxVisibleCharacters = lastCharIndex;
                        },
                        OnAnimationFinished.Invoke);
                    break;
                case TextAppearType.Word:
                    _tmpText.maxVisibleWords = 0;
                    _animationDuration = _wordCount * wordTypeTime;
                    _animation = new SimpleAnimation(_animationDuration,
                        progress =>
                        {
                            var lastWorldIndex = (int)(progress * _wordCount);
                            if (typingSound && _tmpText.maxVisibleWords >= playFrequency + _rememberFrequencyIndex)
                            {
                                _rememberFrequencyIndex = lastWorldIndex;
                                _audioSource.PlayOneShot(typingSound);
                            }
                            _tmpText.maxVisibleWords = lastWorldIndex;
                        },
                        OnAnimationFinished.Invoke);
                    break;
                case TextAppearType.Line:
                    _tmpText.maxVisibleLines = 0;
                    _animationDuration = _lineCount * lineTypeTime;
                    _animation = new SimpleAnimation(_animationDuration,
                        progress =>
                        {
                            var lastLineIndex = (int)(progress * _lineCount);
                            if (typingSound && _tmpText.maxVisibleLines >= playFrequency + _rememberFrequencyIndex)
                            {
                                _rememberFrequencyIndex = lastLineIndex;
                                _audioSource.PlayOneShot(typingSound);
                            }
                            _tmpText.maxVisibleLines = lastLineIndex;
                        },
                        OnAnimationFinished.Invoke);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_animation.IsPlaying) _animation.Update();
        }

        void OnEnable()
        {
            _rememberInfo = _tmpText.textInfo;
            _animation.Play();
        }

        void OnDisable()
        {
            _animation.PauseAndReset();
        }
    }
}
