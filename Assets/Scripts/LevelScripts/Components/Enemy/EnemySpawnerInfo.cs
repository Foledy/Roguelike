using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create Enemy Spawner Info")]
public class EnemySpawnerInfo : ScriptableObject
{
    [Header("Spawn Settings")]
    [Min(0), Tooltip("The wave after which the 'Giant Melee Enemies' appear")] public int GiantMeleeWave;
    [Min(0), Tooltip("The wave after which the 'Shooter Enemies' appear")] public int ShooterMeleeWave;

    [Header("Difficult Settings")] 
    [Min(0)] public int WeakIncreaseAmount;
    [Min(0)] public int GiantIncreaseAmount;
    [Min(0)] public int ShooterIncreaseAmount;

    [Header("Increase Difficult Settings")]
    public bool IsChangingDifficultEnabled;
    [ShowIf(nameof(IsChangingDifficultEnabled))] public DifficultIncrease DifficultIncrease;

    [Space(7.5f)]
    [ShowIf(nameof(IsChangingDifficultEnabled))] public bool EnableChangeWaveRate;
    
    [ShowIf(nameof(IsChangingDifficultEnabled))] 
    [EnableIf(nameof(EnableChangeWaveRate))] public ChangeDifficultRate ChangeDifficultRate;
}

[Serializable]
public struct ChangeDifficultRate
{
    [Min(0)] public int WeakChangeRate;
    [Min(0)] public int GiantChangeRate;
    [Min(0)] public int ShooterChangeRate;
}

[Serializable]
public struct DifficultIncrease
{
    [Min(0)] public int WeakDifficultIncrease;
    [Min(0)] public int GiantDifficultIncrease;
    [Min(0)] public int ShooterDifficultIncrease;
}