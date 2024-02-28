using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new BoosterDropSettings", menuName = "Roguelike/Create Booster Drop Settings", order = 0)]
public class BoosterDropSettings : ScriptableObject
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _protection;
    [SerializeField] private float _weapon;
    [SerializeField] private float _damage;

    public float GetChance(BoosterType type)
    {
        switch(type)
        {
            case BoosterType.Health:
                return _health;
            case BoosterType.Speed:
                return _speed;
            case BoosterType.Protection:
                return _protection;
            case BoosterType.Weapon:
                return _weapon;
            case BoosterType.Damage:
                return _damage;
            
            default:
                throw new ArgumentException("[Booster Drop Settings] Invalid booster type!");
        }
    }
}