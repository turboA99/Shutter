using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class AwarenessManager : MonoBehaviour
    {
        public static AwarenessManager instance;
        [SerializeField] private float maskDurationScale = 3f;

        private Interaction _currentMaskInteraction;
        private NoiseReduced _reductionLevel = NoiseReduced.None;
        private NoiseMade _constantNoiseLevel = NoiseMade.VeryLow;

        private List<IEnumerator> _coroutines = new();

        [SerializeField] private float minAwareness = 0;
        [SerializeField] private float maxAwareness = 100;
        [SerializeField] private float starterAwareness = 0;
        [SerializeField] private float awarenessMultiplier = 2;
        [SerializeField] private float reductionMultiplier = 2;
        [SerializeField] private float reductionIncreaserMultiplier = 0;
        [SerializeField] private float repeatedPenaltyMult = 1;
        [SerializeField] private int maxRepeatsChecked = 5;

        private float _awareness;
        private List<InteractableObject> _sources = new();

        public UnityEvent<float> OnAwarenessChange;
        public UnityEvent OnAwarenessFilled;

        public UnityEvent<Interaction> OnMaskStart;
        public UnityEvent<Interaction> OnMaskEnd;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            _awareness = starterAwareness;

            InteractionManager.instance.OnInteractionDecided.AddListener(Interacted);
            OnMaskEnd.AddListener(UpdateMaskSources);
        }

        // Update is called once per frame
        void Update()
        {
            IncreaseAwarenessPerFrame(DecideAwarenessIncrease(_constantNoiseLevel));
            DecreaseAwarenessPerFrame(DecideAwarenessDecrease(_reductionLevel));
        }

        public float DecideAwarenessIncrease(NoiseMade noiseLevel)
        {
            float finalValue = 0;

            if ((float)_reductionLevel * reductionIncreaserMultiplier != 0)
            {
                finalValue = (float)noiseLevel * awarenessMultiplier / ((float)_reductionLevel * reductionIncreaserMultiplier);
            }
            else
            {
                finalValue = (float)noiseLevel * awarenessMultiplier;
            }

            return finalValue;
        }

        public void DecreaseAwarenessPerFrame(float decrease)
        {
            _awareness = Mathf.Clamp(_awareness - decrease * Time.deltaTime, minAwareness, maxAwareness);
            OnAwarenessChange?.Invoke(_awareness);
            CheckAwareness();
        }

        private void IncreaseAwarenessPerFrame(float increase)
        {
            _awareness = Mathf.Clamp(_awareness + increase * Time.deltaTime, minAwareness, maxAwareness);
            OnAwarenessChange?.Invoke(_awareness);
            CheckAwareness();
        }

        public void IncreaseAwareness(NoiseMade noiseLevel)
        {
            _awareness = Mathf.Clamp(_awareness + DecideAwarenessIncrease(noiseLevel), minAwareness, maxAwareness);

            Debug.Log($"Increased: {_awareness}");

            OnAwarenessChange?.Invoke(_awareness);

            CheckAwareness();
        }

        public void CheckAwareness()
        {
            if (_awareness >= maxAwareness)
            {
                OnAwarenessFilled?.Invoke();
            }
        }

        public void DecreaseAwareness(NoiseReduced reduction)
        {
            _awareness = Mathf.Clamp(_awareness - DecideAwarenessDecrease(reduction), minAwareness, maxAwareness);

            OnAwarenessChange?.Invoke(_awareness);
            Debug.Log(_awareness);

            CheckAwareness();
        }

        public void UpdateMaskSources(Interaction interaction)
        {
            if (_sources.Count >= maxRepeatsChecked)
            {
                _sources.RemoveAt(maxRepeatsChecked - 1);
            }

            _sources.Insert(0, interaction.interactedObject);
        }

        public float DecideAwarenessDecrease(NoiseReduced reduction)
        {
            int amountPrevious = 0;

            foreach (var interactable in _sources)
            {
                if (interactable == _currentMaskInteraction.interactedObject)
                {
                    amountPrevious++;
                }
            }

            if (amountPrevious * repeatedPenaltyMult == 0)
            {
                return (float)reduction * reductionMultiplier;
            }
        
            return (float)reduction * reductionMultiplier / (amountPrevious * repeatedPenaltyMult);
        }

        public void Interacted(Interaction interaction)
        {
            if (interaction.duration != ReductionDuration.None)
            {
                MaskSound(interaction);
            }

            IncreaseAwareness(interaction.noiseLevel);

            Debug.Log($"Awareness sees interaction {interaction.interactedObject}");
        }

        private void MaskSound(Interaction interaction)
        {
            _currentMaskInteraction = interaction;
            _reductionLevel = _currentMaskInteraction.reductionLevel;

            foreach (var coroutine in _coroutines)
            {
                StopCoroutine(coroutine);
                _coroutines.Remove(TimeMask(interaction));
                OnMaskEnd?.Invoke(interaction);
                Debug.Log($"Masker Ended");
            }

            _coroutines.Clear();

            _coroutines.Add(TimeMask(interaction));
            StartCoroutine(_coroutines[0]);

            OnMaskStart?.Invoke(interaction);
        }

        IEnumerator TimeMask(Interaction interaction)
        {
            float timer = (float)interaction.duration * maskDurationScale;

            Debug.Log($"Masker Started for : {timer}");

            yield return new WaitForSeconds(timer);

            _reductionLevel = NoiseReduced.None;
            OnMaskEnd?.Invoke(interaction);
            StopCoroutine(_coroutines[0]);
        

            Debug.Log($"Masker Ended");

            yield break;
        }
    }
}
