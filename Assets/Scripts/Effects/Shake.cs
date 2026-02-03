using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class Shake : MonoBehaviour
    {
        [SerializeField] [Tooltip("How far from the initial position the object can shake to")]
        public float shakeDistance = .1f;

        [SerializeField] [Tooltip("How much time passes between shakes")]
        public float durationBetweenShakes = .1f;

        [SerializeField]
        bool doRandomizeDelay = true;

        float _delay;

        Vector3 _initialPosition;
        float? _previousShakeTime;

        public float ShakeDistance
        {
            get => shakeDistance;
            set => shakeDistance = value;
        }

        public float DurationBetweenShakes
        {
            get => shakeDistance;
            set => shakeDistance = value;
        }

        void Update()
        {

            var timeDifference = Time.time - _previousShakeTime;
            if (timeDifference >= durationBetweenShakes)
            {
                var angle = Random.Range(0, 2 * Mathf.PI);
                var direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
                var distance = Random.Range(0f, Mathf.Clamp(shakeDistance, 0f, float.MaxValue));
                Vector3 newPosition = _initialPosition + direction * distance;
                transform.SetPositionAndRotation(newPosition, transform.rotation);
                _previousShakeTime = Time.time;
            }
        }

        void OnEnable()
        {
            Initialize();
        }

        void OnDisable()
        {
            transform.SetPositionAndRotation(_initialPosition, transform.rotation);
        }

        void Initialize()
        {
            _initialPosition = transform.position;
            _delay = Random.Range(0f, durationBetweenShakes);
            if (doRandomizeDelay) _previousShakeTime = Time.time - _delay;
            else _previousShakeTime = Time.time;
        }
    }
}
