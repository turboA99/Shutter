using UnityEngine;

public class RustedCar : Interactable
{
    private bool isOpen = false;

    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                if (!isOpen)
                {
                    OnInteractionStarted?.Invoke(interactions[0]);
                }
                else
                {
                    OnInteractionStarted?.Invoke(interactions[2]);
                }
                break;
            case InventoryItem.Crowbar:
                OnInteractionStarted?.Invoke(interactions[1]);
                isOpen = true;
                break;
            case InventoryItem.SparkPlug:
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
