using UnityEngine;

[CreateAssetMenu(fileName = "new SpeedBoosterSettings", menuName = "Roguelike/Create Booster/Create SpeedBooster Settings", order = 0)]
public class SpeedBoosterSettings : ScriptableObject
{
    [Range(10f, 60f)] public float SpeedBoosterDuration;
    [Range(1f, 1.5f)] public float SpeedMultiplier;
}