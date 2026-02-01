using Managers;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Tree : Interactable
{
    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                OnInteractionStarted?.Invoke(interactions[0]);
                break;
            case InventoryItem.LongStick:
                OnInteractionStarted?.Invoke(interactions[1]);
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
