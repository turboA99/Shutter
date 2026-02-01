using Managers;
using UnityEngine;

public class Frog2 : Interactable
{
    private bool isIdle = true;

    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                if (isIdle)
                {
                    OnInteractionStarted?.Invoke(interactions[0]);
                    isIdle = false;
                }
                break;
            case InventoryItem.FrogTwo:
                OnInteractionStarted?.Invoke(interactions[1]);
                isIdle = true;
                break;
            case InventoryItem.FrogOne:
                OnInteractionStarted?.Invoke(interactions[2]);
                isIdle = false;
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

    public void MaskStarted(Interaction interaction)
    {
        if (interaction == interactions[3])
        {
            isIdle = false;
            mainSprite.sprite = interactions[0].newSprite;
            outlineSprite.sprite = interactions[0].newOutline;
        }
    }

    public void ReturnBase(Interaction interaction)
    {
        if (interaction == interactions[2] || interaction == interactions[3])
        {
            isIdle = true;
            mainSprite.sprite = interactions[1].newSprite;
            outlineSprite.sprite = interactions[1].newOutline;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();

        AwarenessManager.instance.OnMaskEnd.AddListener(ReturnBase);
        AwarenessManager.instance.OnMaskStart.AddListener(MaskStarted);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        isIdle = true;
        mainSprite.sprite = interactions[1].newSprite;
        outlineSprite.sprite = interactions[1].newOutline;
    }
}
