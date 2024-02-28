using System;
using UnityEngine;
using Zenject;

public class BoosterPoolService
{
    public event Action<BoosterType> OnPickedUp;

    private HealthBooster.Pool _healthPool;
    private SpeedBooster.Pool _speedPool;
    private ProtectionBooster.Pool _protectionPool;
    private WeaponBooster.Pool _weaponPool;
    private DamageBooster.Pool _damagePool;

    public BoosterPoolService(HealthBooster.Pool health, SpeedBooster.Pool speed, ProtectionBooster.Pool protection,
        WeaponBooster.Pool weapon, DamageBooster.Pool damage)
    {
        _healthPool = health;
        _speedPool = speed;
        _protectionPool = protection;
        _weaponPool = weapon;
        _damagePool = damage;
    }

    public void Spawn(BoosterType type, Vector3 position, Quaternion rotation)
    {
        switch (type)
        {
            case BoosterType.Health:
                Spawn(_healthPool, position, rotation);
                break;
            case BoosterType.Speed:
                Spawn(_speedPool, position, rotation);
                break;
            case BoosterType.Protection:
                Spawn(_protectionPool, position, rotation);
                break;
            case BoosterType.Weapon:
                Spawn(_weaponPool, position, rotation);
                break;
            case BoosterType.Damage:
                Spawn(_damagePool, position, rotation);
                break;
        }
    }

    public void Despawn(Booster booster, BoosterType type)
    {
        switch (type)
        {
            case BoosterType.Health:
                _healthPool.Despawn(booster);
                break;
            case BoosterType.Speed:
                _speedPool.Despawn(booster);
                break;
            case BoosterType.Protection:
                _protectionPool.Despawn(booster);
                break;
            case BoosterType.Weapon:
                _weaponPool.Despawn(booster);
                break;
            case BoosterType.Damage:
                _damagePool.Despawn(booster);
                break;
        }
        
        OnPickedUp?.Invoke(type);
    }

    private void Spawn<TPool>(MonoMemoryPool<TPool> pool, Vector3 position, Quaternion rotation)
        where TPool : Booster
    {
        var booster = pool.Spawn();
        
        booster.Subscribe(type => { Despawn(booster, type); });
        booster.Initialize(position, rotation);
    }
}