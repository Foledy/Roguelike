using System;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private Transform _meleeEnemySpawnParent;
    [SerializeField] private Transform _shooterEnemySpawnParent;
    [SerializeField] private WaveTimer _waveTimer;
    
    [Inject] private EnemyPoolService _enemyService;
    [Inject] private WaveDifficultSettings _difficultSettings;
    [Inject] private WaveSettings _waveSettings;

    private int _weekAmount;
    private int _giantAmount;
    private int _shooterAmount;
    private float _timeToPassWave;
    private Result _result;
    private List<Vector3> _meleeSpawnPoints;
    private List<Vector3> _shooterSpawnPoints;

    private void Awake()
    {
        _meleeSpawnPoints = new List<Vector3>();
        _shooterSpawnPoints = new List<Vector3>();
        _result = new Result();
        _timeToPassWave = _waveSettings.TimeToPass;

        foreach (Transform spawnPoint in _meleeEnemySpawnParent)
        {
            _meleeSpawnPoints.Add(spawnPoint.position);
        }

        foreach (Transform spawnPoint in _shooterEnemySpawnParent)
        {
            _shooterSpawnPoints.Add(spawnPoint.position);
        }

        _enemyService.OnEnemyKilled += OnEnemyKilled;
        _enemyService.OnAllEnemiesDestroyed += OnWaveCleared;
        _waveTimer.OnTimeDecreased += OnTimeDecreased;
        StartTimer(_waveSettings.IntervalBetweenWaves);
    }

    private void OnDisable()
    {
        _enemyService.OnEnemyKilled -= OnEnemyKilled;
        _enemyService.OnAllEnemiesDestroyed -= OnWaveCleared;
        _waveTimer.OnTimeDecreased -= OnTimeDecreased;
    }

    private void OnWaveCleared()
    {
        _weekAmount += GetRandomValue(EnemyType.WeakMelee);
        _giantAmount += GetRandomValue(EnemyType.GiantMelee);
        _shooterAmount += GetRandomValue(EnemyType.Shooter);

        if (_waveSettings.IncreaseTimeToPass == true)
        {
            _timeToPassWave += _waveSettings.AdditionalTime;
        }

        _waveTimer.OnTimeUp -= GameOver;
        _waveTimer.OnTimeUp += OnWaveStarting;
        
        StartTimer(_waveSettings.IntervalBetweenWaves);
    }

    private void OnWaveStarting()
    {
        _waveTimer.OnTimeUp += GameOver;
        _waveTimer.OnTimeUp -= OnWaveStarting;
        
        StartWave();
    }

    private void StartWave()
    {
        _enemyService.Spawn(_weekAmount, EnemyType.WeakMelee, _meleeSpawnPoints, Quaternion.identity);
        _enemyService.Spawn(_giantAmount, EnemyType.GiantMelee, _meleeSpawnPoints, Quaternion.identity);
        _enemyService.Spawn(_shooterAmount, EnemyType.Shooter, _shooterSpawnPoints, Quaternion.identity);
        
        StartTimer(_timeToPassWave);
    }

    private int GetRandomValue(EnemyType type)
    {
        int result = 0;
        
        switch (type)
        {
            case EnemyType.WeakMelee:
                result = Random.Range(_difficultSettings.WeekEnemy.MinIncrease, _difficultSettings.WeekEnemy.MaxIncrease); 
                break;
            case EnemyType.GiantMelee:
                result = Random.Range(_difficultSettings.GiantEnemy.MinIncrease, _difficultSettings.GiantEnemy.MaxIncrease);
                break;
            case EnemyType.Shooter:
                result = Random.Range(_difficultSettings.ShooterEnemy.MinIncrease, _difficultSettings.ShooterEnemy.MaxIncrease);
                break;
        }

        return result;
    }

    private void OnEnemyKilled(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.WeakMelee:
                _result.WeekKilled += 1;
                break;
            case EnemyType.GiantMelee:
                _result.GiantKilled += 1;
                break;
            case EnemyType.Shooter:
                _result.ShooterKilled += 1;
                break;
        }
    }

    private void OnTimeDecreased() => _result.LivedSeconds += 1;
    
    private void GameOver() => ResultScene.Load(_result);

    private void StartTimer(float seconds) => _waveTimer.Enable(seconds);
}