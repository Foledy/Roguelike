using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthAbility))]
public class HealthHandler : MonoBehaviour
{
    private Queue<KeyValuePair<HealthActionType, float>> _actionsQueue;

    private void Start()
    {
        _actionsQueue = new Queue<KeyValuePair<HealthActionType, float>>();
    }

    public void ClearQueue() => _actionsQueue.Clear();

    public bool TryGetAction(out KeyValuePair<HealthActionType, float> action)
    {
        if (_actionsQueue.Count > 0)
        {
            action = _actionsQueue.Peek();
            
            return true;
        }

        action = new();
        
        return false;
    }
    
    public void AddActionToQueue(HealthActionType type, float value) 
        => _actionsQueue.Enqueue(new KeyValuePair<HealthActionType, float>(type, value));
}

public enum HealthActionType
{
    None = 0,
    Heal = 1,
    TakeDamage = 2
}