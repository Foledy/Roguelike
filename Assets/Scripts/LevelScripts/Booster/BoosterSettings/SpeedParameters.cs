using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new SpeedBoosterSettings", menuName = "Roguelike/Create Booster/Create SpeedBooster Settings", order = 0)]
public class SpeedParameters : ScriptableObject
{
    [FormerlySerializedAs("SpeedBoosterDuration")] [Range(10f, 60f)] public float Duration;
    [Range(1f, 1.5f)] public float SpeedMultiplier;
}