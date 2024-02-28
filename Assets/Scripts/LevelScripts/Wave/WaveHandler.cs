using System;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private WaveTimer _waveTimer;
    [SerializeField] private EnemySpawner _spawner;
    
    [Inject] private WaveDifficultSettings _difficultSettings;
    [Inject] private WaveSettings _waveSettings;
    [Inject] private EnemyPoolService _enemyService;
    
    private int _weekAmount;
    private int _giantAmount;
    private int _shooterAmount;
    private float _timeToPassWave;
    private Result _result;

    private void Start()
    {
        _result = new Result();
        _timeToPassWave = _waveSettings.TimeToPass;

        _enemyService.OnEnemyKilled += OnEnemyKilled;
        _enemyService.OnAllEnemiesDestroyed += OnWaveCleared;
        
        _waveTimer.OnTimeDecreased += OnTimeDecreased;
        _waveTimer.OnTimeUp += GameOver;
        OnWaveCleared();
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
        _spawner.SpawnEnemies(_weekAmount, _giantAmount, _shooterAmount);
        
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

    private void OnTimeDecreased(float increasedTime) => _result.LivedSeconds += increasedTime;
    
    private void GameOver() => ResultScene.Load(_result);

    private void StartTimer(float seconds) => _waveTimer.Enable(seconds);
}