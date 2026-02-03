using Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager instance;
        [SerializeField] Interaction winningInteraction;

        public UnityEvent<Interaction> OnInteractionDecided;
        public UnityEvent OnWin;

        void Awake()
        {
            instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void Interacted(Interaction interaction)
        {
            if (interaction == winningInteraction)
            {
                OnWin?.Invoke();
            }

            OnInteractionDecided?.Invoke(interaction);
        }
    }
}
