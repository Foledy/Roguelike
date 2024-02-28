using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [Header("Boosters")] 
    [SerializeField] private HealthParameters _healthParameters;
    [SerializeField] private SpeedParameters _speedParameters;
    [SerializeField] private ProtectionParameters _protetionParameters;
    [SerializeField] private WeaponParameters _weaponParameters;
    [SerializeField] private DamageParameters _damageParameters;
    
    public BoostersParameters BoostersParameters { get; private set; }
    public CharacterSettings CharacterSettings { get; private set; }
    public BoosterData BoosterData { get; private set; } = new();
    
    private Queue<BoosterType> _boosterQueue;
    
    private void Start()
    {
        _boosterQueue = new Queue<BoosterType>();

        BoostersParameters = new BoostersParameters(_healthParameters, _speedParameters, _protetionParameters, _weaponParameters, _damageParameters);
    }

    public void SetCharacter(CharacterSettings character)
    {
        CharacterSettings = character;
        
        GetComponent<HealthAbility>().SetHealth(CharacterSettings.StartHealth, CharacterSettings.MaxHealth);
    }

    public void SetBoostersParameters(BoostersParameters settings) => BoostersParameters = settings;

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