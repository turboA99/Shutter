using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private InventoryItem currentItem = InventoryItem.Empty;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractionManager.instance.OnInteractionDecided.AddListener(Interacted);
    }

    public void Interacted(Interaction interaction)
    {
        if (interaction.newItem == InventoryItem.None)
        {
            return;
        }

        currentItem = interaction.newItem;
        Debug.Log($"New Item: {currentItem}");
    }

    public InventoryItem GetItem()
    {
        return currentItem;
    }
}
