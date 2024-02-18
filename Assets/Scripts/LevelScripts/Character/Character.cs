using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [Inject] public CharacterSettings CharacterSettings { get; private set; }
    [Inject] public BoostersSettings BoostersSettings { get; private set; }
    
    private Queue<BoosterType> _boosterQueue;

    private void Start()
    {
        _boosterQueue = new Queue<BoosterType>();
    }

    public void SetCharacter(CharacterSettings character) => CharacterSettings = character;

    public void AddBoosterToQueue(BoosterType type)
    {
        _boosterQueue.Enqueue(type);
    }

    public bool TryGetBoosterFromQueue(out BoosterType type)
    {
        type = 0;
        
        if (_boosterQueue.Count == 0)
            return false;
        
        type = _boosterQueue.Dequeue();
        return true;
    }
}