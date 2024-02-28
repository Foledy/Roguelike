using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Booster : MonoBehaviour
{
    private event Action<BoosterType> OnPickedUp;

    private List<Action<BoosterType>> _subscribers = new();
    
    protected BoosterType _type;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        
        SetBoosterType();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character) == true)
        {
            OnPickedUp?.Invoke(_type);
        }
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        var boosterTransform = transform;

        boosterTransform.position = position;
        boosterTransform.rotation = rotation;
    }

    public void Subscribe(Action<BoosterType> callback)
    {
        _subscribers.Add(callback);

        OnPickedUp += callback;
    }

    private void UnSubscribeAll()
    {
        if (_subscribers.Count == 0)
        {
            return;
        }
        
        foreach (var sub in _subscribers)
        {
            OnPickedUp -= sub;
        }
        
        _subscribers.Clear();
    }
    
    protected abstract void SetBoosterType();
}