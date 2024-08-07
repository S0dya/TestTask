using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubjectMonoBehaviour : MonoBehaviour
{
    private Dictionary<EventEnum, Action> _eventActionDict = new();

    protected virtual void Init(Dictionary<EventEnum, Action> eventActionDict) => _eventActionDict = eventActionDict;

    protected virtual void OnEnable() => Observer.OnEvent += OnEvent;
    protected virtual void OnDisable() => Observer.OnEvent -= OnEvent;

    protected virtual void OnEvent(EventEnum eventEnum)
    {
        if (_eventActionDict.ContainsKey(eventEnum)) _eventActionDict[eventEnum]?.Invoke();
    }
}
