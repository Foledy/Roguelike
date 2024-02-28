using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyPoolService
{
    public event Action<EnemyType> OnEnemyKilled;
    public event Action<Vector3> OnEnemyWithBoosterKilled;
    public event Action OnAllEnemiesDestroyed;
    
    public int EnemiesLeft { get; private set; }
    
    private WeekEnemy.Pool _weekPool;
    private GiantEnemy.Pool _giantPool;
    private ShooterEnemy.Pool _shooterPool;

    public EnemyPoolService(WeekEnemy.Pool weekPool, GiantEnemy.Pool giantPool, ShooterEnemy.Pool shooterPool)
    {
        _weekPool = weekPool;
        _giantPool = giantPool;
        _shooterPool = shooterPool;
    }

    public void Spawn(int count, EnemyType type, IReadOnlyList<Vector3> positions, Quaternion rotation)
    {
        switch (type)
        {
            case EnemyType.WeakMelee:
                SpawnEnemy(count, _weekPool, positions, rotation, type);
                break;
            case EnemyType.GiantMelee:
                SpawnEnemy(count, _giantPool, positions, rotation, type);
                break;
            case EnemyType.Shooter:
                SpawnEnemy(count, _shooterPool, positions, rotation, type);
                break;
            
            default:
                throw new ArgumentException("[Enemy Pool Service] Bad enemy type");
        }
    }

    public void Despawn(Enemy enemy, EnemyType type)
    {
        switch (type)
        {
            case EnemyType.WeakMelee:
                _weekPool.Despawn(enemy);
                break;
            case EnemyType.GiantMelee:
                _giantPool.Despawn(enemy);
                break;
            case EnemyType.Shooter:
                _shooterPool.Despawn(enemy);
                break;
        }

        EnemiesLeft -= 1;
        OnEnemyKilled?.Invoke(type);
        OnEnemyWithBoosterKilled?.Invoke(enemy.transform.position);

        if (EnemiesLeft == 0)
        {
            OnAllEnemiesDestroyed?.Invoke();
        }
    }

    public void DespawnAll()
    {
        Observable.Start(() =>
        {
            _weekPool.Clear();
            _giantPool.Clear();
            _shooterPool.Clear();
        });
    }

    private void SpawnEnemy<TPool>(int count, MonoMemoryPool<TPool> pool, IReadOnlyList<Vector3> positions, Quaternion rotation, EnemyType type) 
        where TPool : Enemy
    {
        for (int i = 0; i < count; i++)
        {
            var enemy = pool.Spawn();

            enemy.Subcribe(() => { Despawn(enemy, type); });
            enemy.Initialize(positions[Random.Range(0, positions.Count)], rotation);
        }

        EnemiesLeft += count;
    }
}