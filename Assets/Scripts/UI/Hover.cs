using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        UnityEvent onMouseEnter;
        [SerializeField]
        UnityEvent onMouseExit;

        void OnMouseEnter()
        {
            onMouseEnter?.Invoke();
        }

        void OnMouseExit()
        {
            onMouseExit?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseExit?.Invoke();
        }
    }
}
