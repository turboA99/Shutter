using System.Collections.Generic;
using Interactions;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected InteractableObject thisInteractable;
    [SerializeField] protected SpriteRenderer mainSprite;
    [SerializeField] protected SpriteRenderer outlineSprite;

    [SerializeField] protected List<Interaction> interactions;

    public UnityEvent<Interaction> OnInteractionStarted;

    protected void Start()
    {
        OnInteractionStarted.AddListener(InteractionManager.instance.Interacted);
        InteractionManager.instance.OnInteractionDecided.AddListener(Interacted);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DecideInteraction();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineSprite.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outlineSprite.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        OnInteractionStarted.RemoveAllListeners();
        InteractionManager.instance.OnInteractionDecided.RemoveListener(Interacted);
    }

    public abstract void DecideInteraction();

    public abstract void Interacted(Interaction interaction);
}
