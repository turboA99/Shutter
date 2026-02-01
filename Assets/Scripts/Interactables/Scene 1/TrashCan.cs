using UnityEngine;

public class TrashCan : Interactable
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

    public void MaskEnded(Interaction interaction)
    {
        if (interaction == interactions[1] || interaction == interactions[2] || interaction == interactions[3])
        {
            mainSprite.sprite = interactions[0].newSprite;
            outlineSprite.sprite = interactions[0].newOutline;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        AwarenessManager.instance.OnMaskEnd.AddListener(MaskEnded);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        mainSprite.sprite = interactions[0].newSprite;
        outlineSprite.sprite = interactions[0].newOutline;
    }
}
