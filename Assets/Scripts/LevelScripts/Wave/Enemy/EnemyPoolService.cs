using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemyPoolService
{
    public event Action<EnemyType> OnEnemyKilled;
    public event Action OnAllEnemiesDestroyed;
    
    public int EnemiesLeft { get; private set; }
    
    private WeekEnemy.Pool _weekPool;
    private GiantEnemy.Pool _giantPool;
    private ShooterEnemy.Pool _shooterPool;
    private List<Enemy> _enemies;

    public EnemyPoolService(WeekEnemy.Pool weekPool, GiantEnemy.Pool giantPool, ShooterEnemy.Pool shooterPool)
    {
        _enemies = new List<Enemy>();
        _weekPool = weekPool;
        _giantPool = giantPool;
        _shooterPool = shooterPool;
    }

    public void Spawn(int count, EnemyType type, List<Vector3> positions, Quaternion rotation)
    {
        switch (type)
        {
            case EnemyType.WeakMelee:
                SpawnEnemy(count, _weekPool, positions, rotation);
                break;
            case EnemyType.GiantMelee:
                SpawnEnemy(count, _giantPool, positions, rotation);
                break;
            case EnemyType.Shooter:
                SpawnEnemy(count, _shooterPool, positions, rotation);
                break;
        }
    }

    public void Despawn(Enemy enemy, EnemyType type)
    {
        _enemies.Remove(enemy);

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

    private void SpawnEnemy<TPool>(int count, MonoMemoryPool<TPool> pool, List<Vector3> positions, Quaternion rotation) 
        where TPool : Enemy
    {
        for (int i = 0; i < count; i++)
        {
            var enemy = pool.Spawn();
        
            _enemies.Add(enemy);
            enemy.Initialize(positions[i], rotation);
        }

        EnemiesLeft += count;
    }
}