using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource interactableSource;
        [SerializeField] private AudioSource maskerSource;
        [SerializeField] private AudioSource ambientSource;

        public static AudioManager instance;

        private void Awake()
        {
            instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InteractionManager.instance.OnInteractionDecided.AddListener(Interacted);
            AwarenessManager.instance.OnMaskStart.AddListener(PlayMasker);
            AwarenessManager.instance.OnMaskEnd.AddListener(StopMasker);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Interacted(Interaction interaction)
        {
            Debug.Log($"Audio Manager sees interaction {interaction.interactedObject}");

            if (interaction.duration == 0)
            {
                interactableSource.PlayOneShot(interaction.clipToPlay);
            }
        }

        public void PlayMasker(Interaction interaction)
        {
            maskerSource.PlayOneShot(interaction.clipToPlay);
        }

        public void StopMasker(Interaction interaction)
        {
            maskerSource.Stop();
        }
    }
}
