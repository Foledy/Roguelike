using UnityEngine;

[CreateAssetMenu(fileName = "new ProtectionBoosterSettings", menuName = "Roguelike/Create Booster/Create ProtectionBooster Settings", order = 0)]
public class ProtectionBoosterSettings : ScriptableObject
{
    [Range(1f, 2f)] public float ProtectionMultiplier;
    [Range(10f, 60f)] public float ProtectionBoosterDuration;
}