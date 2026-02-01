using UnityEngine;

public class GasCan : Interactable
{
    private bool isFunneled = false;
    private bool isUsed = false;

    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                if (!isFunneled)
                {
                    OnInteractionStarted?.Invoke(interactions[0]);
                }
                else if (!isUsed)
                {
                    OnInteractionStarted?.Invoke(interactions[3]);
                    isUsed = true;
                }
                break;
            case InventoryItem.BrokenGas:
                OnInteractionStarted?.Invoke(interactions[1]);
                break;
            case InventoryItem.Funnel:
                OnInteractionStarted?.Invoke(interactions[2]);
                isFunneled = true;
                break;
            case InventoryItem.FunneledGas:
                OnInteractionStarted?.Invoke(interactions[4]);
                isUsed = false;
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
