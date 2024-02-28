using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new ProtectionBoosterSettings", menuName = "Roguelike/Create Booster/Create ProtectionBooster Settings", order = 0)]
public class ProtectionParameters : ScriptableObject
{
    [FormerlySerializedAs("ProtectionBoosterDuration")] [Range(10f, 60f)] public float Duration;
    [Range(1f, 2f)] public float ProtectionMultiplier;
}