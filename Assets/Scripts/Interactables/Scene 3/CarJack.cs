using Interactions;
using Managers;

public class CarJack : Interactable
{
    bool isGrabbed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                if (!isGrabbed)
                {
                    OnInteractionStarted?.Invoke(interactions[0]);
                    outlineSprite.enabled = false;
                    isGrabbed = true;
                }
                break;
            case InventoryItem.Carjack:
                OnInteractionStarted?.Invoke(interactions[1]);
                isGrabbed = false;
                outlineSprite.enabled = true;
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
}
