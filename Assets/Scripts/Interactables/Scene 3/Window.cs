using System.Collections;
using Interactions;
using Managers;
using UnityEngine;

public class Window : Interactable
{
    [SerializeField]
    Sprite closedFrame;

    [SerializeField]
    Sprite closedHighlight;

    [SerializeField]
    Sprite middleFrame;

    [SerializeField]
    Sprite middleHighlight;

    [SerializeField]
    Sprite lastFrame;

    [SerializeField]
    Sprite lastHighlight;

    [SerializeField]
    float timeBetweenFrameS;

    bool isIdle = true;
    Coroutine reverseAnimation;

    Coroutine startAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        AwarenessManager.instance.OnMaskEnd.AddListener(MaskEnded);
    }

    void OnDisable()
    {
        isIdle = true;
        mainSprite.sprite = closedFrame;
        outlineSprite.sprite = closedHighlight;
    }

    public override void DecideInteraction()
    {
        switch (InventoryManager.instance.GetItem())
        {
            case InventoryItem.Empty:
                if (isIdle)
                {
                    OnInteractionStarted?.Invoke(interactions[0]);
                }
                break;
            case InventoryItem.LongStick:
                OnInteractionStarted?.Invoke(interactions[1]);
                isIdle = false;
                startAnimation = StartCoroutine(WolfForwardAnimation());
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
        if (interaction == interactions[1])
        {
            isIdle = true;
            mainSprite.sprite = middleFrame;
            outlineSprite.sprite = middleHighlight;
            reverseAnimation = StartCoroutine(WolfBackwardAnimation());
        }
    }

    IEnumerator WolfForwardAnimation()
    {
        yield return new WaitForSeconds(timeBetweenFrameS);

        mainSprite.sprite = lastFrame;
        outlineSprite.sprite = lastHighlight;

        yield break;
    }

    IEnumerator WolfBackwardAnimation()
    {
        yield return new WaitForSeconds(timeBetweenFrameS - .1f);

        mainSprite.sprite = closedFrame;
        outlineSprite.sprite = closedHighlight;

        yield break;
    }
}
