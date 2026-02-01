using Managers;
using UnityEngine;

public class Cooking : Interactable
{
    private bool isGrabbed = false;

    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                if (!isGrabbed)
                {
                    OnInteractionStarted?.Invoke(interactions[0]);
                    isGrabbed = true;
                }
                break;
            case InventoryItem.Funnel:
                OnInteractionStarted?.Invoke(interactions[1]);
                isGrabbed = false;
                break;
        }
    }

    public override void Interacted(Interaction interaction)
    {
        if (interaction.interactedObject == thisInteractable)
        {
            mainSprite.sprite = interaction.newSprite;
            outlineSprite.sprite = interaction.newOutline;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
