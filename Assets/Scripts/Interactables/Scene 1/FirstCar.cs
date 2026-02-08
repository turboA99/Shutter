using Interactions;

public class FirstCar : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
    }

    public override void DecideInteraction()
    {
        OnInteractionStarted?.Invoke(interactions[0]);
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
