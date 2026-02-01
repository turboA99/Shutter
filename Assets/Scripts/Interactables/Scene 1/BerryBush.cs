using UnityEngine;

public class BerryBush : Interactable
{
    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                OnInteractionStarted?.Invoke(interactions[0]);
                break;
            case InventoryItem.SmallBerry:
                OnInteractionStarted?.Invoke(interactions[1]);
                break;
            case InventoryItem.MediumBerry:
                OnInteractionStarted?.Invoke(interactions[2]);
                break;
            case InventoryItem.LargeBerry:
                OnInteractionStarted?.Invoke(interactions[3]);
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
}

