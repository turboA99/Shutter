using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InteractableType thisInteractable;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

public enum InteractableType
{
    FixableCar
}
