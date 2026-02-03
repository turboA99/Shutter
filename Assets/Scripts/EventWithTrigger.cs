using UnityEngine;
using UnityEngine.Events;

public class EventWithTrigger : MonoBehaviour
{
    [SerializeField] string eventName;
    [SerializeField] UnityEvent events;

    public void Trigger()
    {
        events?.Invoke();
    }
}
