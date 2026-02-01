using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    static public InteractionManager instance;
    [SerializeField] Interaction winningInteraction;

    public UnityEvent<Interaction> OnInteractionDecided;
    public UnityEvent OnWin;

    private void Awake()
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

        Debug.Log($"interaction manager: {interaction}");

        OnInteractionDecided?.Invoke(interaction);
    }
}
