using System;
using Interactions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameInventory : MonoBehaviour
    {
        [SerializeField] Image itemVisual;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void OnEnable()
        {
            itemVisual.enabled = false;
        }

        public void OnInteractionHappened(Interaction interaction)
        {
            switch (interaction.newItem)
            {
                case InventoryItem.None or InventoryItem.Empty:
                    itemVisual.enabled = false;
                    break;
                default:
                    itemVisual.enabled = true;
                    itemVisual.sprite = interaction.inventorySprite;
                    break;
            }
        }
        
    }
}
