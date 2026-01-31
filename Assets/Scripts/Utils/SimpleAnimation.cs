using System;
using UnityEngine;

namespace Utils
{
    public class SimpleAnimation
    {
        public AnimationState AnimationState { get; private set; } = AnimationState.NotStarted;
        public Action OnAnimationFinished;
        public bool IsPlaying => AnimationState != AnimationState.NotStarted;

        float _animationStartTime = Time.time;
        readonly float _animationDuration;
        float _currentlyPlayed;
        readonly Action<float> _animationFunction;
        
        public SimpleAnimation(float animationDuration, Action<float> animationFunction, Action onAnimationFinished = null)
        {
            _animationFunction = animationFunction;
            _animationDuration = animationDuration;
            OnAnimationFinished = onAnimationFinished;
        }

        public void Play()
        {
            switch (AnimationState)
            {
                case AnimationState.Paused:
                    _animationStartTime = Time.time - _currentlyPlayed;
                    AnimationState = AnimationState.Playing;
                    break;
                case AnimationState.NotStarted or AnimationState.Finished:
                    _animationStartTime = Time.time;
                    _currentlyPlayed = 0;
                    AnimationState = AnimationState.Playing;
                    break;
            }
        }

        public void Pause()
        {
            AnimationState = AnimationState.Paused;
        }

        public void Reset()
        {
            AnimationState = AnimationState.NotStarted;
            _animationStartTime = Time.time;
            _currentlyPlayed = 0;
        }
        
        public void Update()
        {
            switch (AnimationState)
            {
                case AnimationState.Playing:
                    _currentlyPlayed = Time.time - _animationStartTime;
                    var progress = Mathf.Clamp(_currentlyPlayed /_animationDuration, 0f, 1f);
                    _animationFunction?.Invoke(progress);
                    if (progress >= 1f)
                    {
                        AnimationState = AnimationState.NotStarted;
                        OnAnimationFinished?.Invoke();
                    }
                    break;
            }
        }
        public void PauseAndReset()
        {
            Pause();
            Reset();
        }
    }
    public enum AnimationState
    {
        NotStarted,
        Playing,
        Paused,
        Finished
    }
}
