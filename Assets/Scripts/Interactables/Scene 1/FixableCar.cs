using Interactions;
using Managers;
using UnityEngine;

public class FixableCar : Interactable
{
    [SerializeField] string baseInspection = "I can use this car to escape. It's missing";
    bool hasGas;
    bool hasSpark;
    bool hasTire;
    bool isLifted;

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
                if (hasGas && hasSpark && hasTire)
                {
                    OnInteractionStarted?.Invoke(interactions[7]);
                }


                else
                {
                    Interaction newInteraction = interactions[0];
                    newInteraction.characterThoughts = baseInspection;
                    if (!hasGas)
                    {
                        newInteraction.characterThoughts += " gas";
                    }


                    if (!hasSpark && !hasGas)
                    {
                        newInteraction.characterThoughts += ", a spark plug";
                    }
                    else if (!hasSpark)
                    {
                        newInteraction.characterThoughts += " a spark plug";
                    }


                    if (!hasTire && (!hasGas || !hasSpark))
                    {
                        newInteraction.characterThoughts += ", a tire";
                    }
                    else if (!hasTire)
                    {
                        newInteraction.characterThoughts += " a tire";
                    }

                    newInteraction.characterThoughts += ".";
                    OnInteractionStarted?.Invoke(newInteraction);
                }

                break;
            case InventoryItem.BrokenGas:
                OnInteractionStarted?.Invoke(interactions[1]);
                break;
            case InventoryItem.FunneledGas:
                OnInteractionStarted?.Invoke(interactions[2]);
                hasGas = true;
                break;
            case InventoryItem.SparkPlug:
                OnInteractionStarted?.Invoke(interactions[3]);

                hasSpark = true;
                break;
            case InventoryItem.Tire:
                if (!isLifted)
                {
                    OnInteractionStarted?.Invoke(interactions[4]);
                }
                else
                {
                    OnInteractionStarted?.Invoke(interactions[6]);
                    hasTire = true;
                }

                break;
            case InventoryItem.Carjack:
                isLifted = true;
                OnInteractionStarted?.Invoke(interactions[5]);
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
