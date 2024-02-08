using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    bool NeedSpawnEnemies { get; }
    EnemySpawnerInfo EnemySpawnerInfo { get; }
    void SpawnEnemies(EnemySpawnData enemySpawnData);
    void DestroyAllEnemies();
}