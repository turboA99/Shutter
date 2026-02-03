using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSequence : MonoBehaviour
{
    public List<UnityEvent> EventSequenceEvents = new();
    public UnityEvent OnEventSequenceFinished;
    public UnityEvent OnStepCompleted;

    int _currentEventIndex = -1;

    void Start()
    {

    }

    public void Advance()
    {
        _currentEventIndex++;

        if (_currentEventIndex < EventSequenceEvents.Count)
        {
            EventSequenceEvents[_currentEventIndex]?.Invoke();
            OnStepCompleted?.Invoke();
        }

        if (_currentEventIndex == EventSequenceEvents.Count - 1)
        {
            OnEventSequenceFinished?.Invoke();
        }
    }
}
