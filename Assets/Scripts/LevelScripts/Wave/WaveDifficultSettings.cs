using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new Difficult Settings", menuName = "Roguelike/Create Difficult Settings", order = 0)]
public class WaveDifficultSettings : ScriptableObject
{
    public DifficultSettings WeekEnemy;
    public DifficultSettings GiantEnemy;
    public DifficultSettings ShooterEnemy;
}

[Serializable]
public struct DifficultSettings
{
    public int MaxIncrease;
    public int MinIncrease;
}