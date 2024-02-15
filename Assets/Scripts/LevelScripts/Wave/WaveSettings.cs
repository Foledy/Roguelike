using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Settings", menuName = "Roguelike/Create new Wave Settings", order = 0)]
public class WaveSettings : ScriptableObject
{
    [Min(0)] public float TimeToPass;
    [Min(0)] public float IntervalBetweenWaves;
    public bool IncreaseTimeToPass;
    [ShowIf(nameof(IncreaseTimeToPass)), Min(0)] public float AdditionalTime;
}