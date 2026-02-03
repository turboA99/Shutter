using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventAfterDelay : MonoBehaviour
{
    [SerializeField] bool playOnEnable = true;
    [SerializeField] float delay = 1.0f;
    [SerializeField] UnityEvent events;
    bool _wasTriggered;

    void OnEnable()
    {
        if (playOnEnable) Trigger();
    }

    void Trigger()
    {
        if (_wasTriggered) return;
        StartCoroutine(TriggerCoroutine());
        _wasTriggered = true;
    }

    IEnumerator TriggerCoroutine()
    {
        yield return new WaitForSeconds(delay);
        events?.Invoke();
    }

    void ResetTrigger()
    {
        _wasTriggered = false;
    }
}
