using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventAfterDelay : MonoBehaviour
{
    [SerializeField] float delay = 1.0f;
    [SerializeField] UnityEvent events;
    [SerializeField] bool playOnEnable = true;

    void OnEnable()
    {
        if (playOnEnable) Trigger();
    }

    void Trigger()
    {
        StartCoroutine(TriggerCoroutine());
    }
    
    IEnumerator TriggerCoroutine()
    {
        yield return new WaitForSeconds(delay);
        events?.Invoke();
    }
}
