using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _meleeEnemySpawnParent;
    [SerializeField] private Transform _shooterEnemySpawnParent;
    [Inject] private EnemyPoolService _enemyService;
    
    private List<Vector3> _shooterSpawnPoints;
    private List<Vector3> _meleeSpawnPoints;

    private void Awake()
    {
        _meleeSpawnPoints = new List<Vector3>();
        _shooterSpawnPoints = new List<Vector3>();
        foreach (Transform spawnPoint in _meleeEnemySpawnParent)
        {
            _meleeSpawnPoints.Add(spawnPoint.position);
        }

        foreach (Transform spawnPoint in _shooterEnemySpawnParent)
        {
            _shooterSpawnPoints.Add(spawnPoint.position);
        }
    }

    public void SpawnEnemies(int weekAmount, int giantAmount, int shooterAmount)
    {
        _enemyService.Spawn(weekAmount, EnemyType.WeakMelee, _meleeSpawnPoints, Quaternion.identity);
        _enemyService.Spawn(giantAmount, EnemyType.GiantMelee, _meleeSpawnPoints, Quaternion.identity);
        _enemyService.Spawn(shooterAmount, EnemyType.Shooter, _shooterSpawnPoints, Quaternion.identity);
    }
}