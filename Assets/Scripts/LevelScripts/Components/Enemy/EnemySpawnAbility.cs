using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Zenject;

public class EnemySpawnAbility : MonoBehaviour, IConvertGameObjectToEntity, IEnemySpawner
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject _weakMeleePrefab;
    [SerializeField] private GameObject _giantMeleePrefab;
    [SerializeField] private GameObject _shooterPrefab;

    [Header("Spawn Positions")] 
    [SerializeField] private Transform[] _meleeEnemySpawnPoints;
    [SerializeField] private Transform[] _shooterEnemySpawnPoints;
    
    [Inject] public EnemySpawnerInfo EnemySpawnerInfo { get; }
    public bool NeedSpawnEnemies { get; private set; }
    
    private int _currentWave = int.MaxValue;
    private int _enemiesAmount = int.MaxValue;
    private List<GameObject> _enemiesList;


    private void OnEnable()
    {
        _enemiesList = new List<GameObject>();
        NeedSpawnEnemies = true;
    }

    private void OnDisable()
    {
        _enemiesList?.Clear();
        NeedSpawnEnemies = false;
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EnemySpawnData());
    }
    
    public void SpawnEnemies(EnemySpawnData enemySpawnData)
    {
        var wave = enemySpawnData.Wave;
    
        SpawnEnemies(enemySpawnData.WeakMeleeData, wave);
        SpawnEnemies(enemySpawnData.GiantMeleeData, wave);
        SpawnEnemies(enemySpawnData.ShooterData, wave);

        NeedSpawnEnemies = false;
    }

    public void DestroyAllEnemies()
    {
        foreach (var enemy in _enemiesList)
        {
            if (enemy != null)
            {
                _enemiesList.Remove(enemy);
                Destroy(enemy);
            }
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        _enemiesAmount -= 1;
        _enemiesList.Remove(enemy);

        if (_enemiesAmount == 0)
        {
            NeedSpawnEnemies = true;
        }
    }
    
    private void SpawnEnemies(EnemyData enemyData, int wave)
    {
        if (enemyData.Amount > 0)
        {
            if (wave != _currentWave)
            {
                _enemiesList?.Clear();
                _currentWave = wave;
                _enemiesAmount = enemyData.Amount;
            }
            else
            {
                _enemiesAmount += enemyData.Amount;
            }

            switch (enemyData.EnemyType)
            {
                case EnemyType.WeakMelee:
                    break;
                case EnemyType.GiantMelee:
                    break;
                case EnemyType.Shooter:
                    break;
            }
        }
    }
}