using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [Inject] public BoostersSettings BoostersSettings { get; private set; }
    [Inject] public CharacterSettings CharacterSettings { get; private set; }
    
    private Queue<BoosterType> _boosterQueue;

    private void Start()
    {
        _boosterQueue = new Queue<BoosterType>();
    }

    public void AddBoosterToQueue(BoosterType type)
    {
        _boosterQueue.Enqueue(type);
    }

    public BoosterType GetBoosterFromQueue()
    {
        if (_boosterQueue.Count == 0)
            return 0;

        return _boosterQueue.Dequeue();
    }
}