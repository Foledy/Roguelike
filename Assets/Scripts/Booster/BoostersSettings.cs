using UnityEngine;

[CreateAssetMenu(fileName = "Boosters Settings", menuName = "Create new boosters settings", order = 0)]
public class BoostersSettings : ScriptableObject
{
    [Header("Health Booster")] 
    public float HealthRecovery;

    [Header("Protection Booster")] 
    [Range(1f, 2f)] public float ProtectionMultiplier;
    [Range(10f, 60f)] public float ProtectionBoosterDuration;

    [Header("Speed Booster")] 
    [Range(1f, 1.5f)] public float SpeedMultiplier;
    [Range(10f, 60f)] public float SpeedBoosterDuration;

    [Header("Damage Booster")] 
    [Range(1f, 3f)] public float DamageMultiplier;
    [Range(10f, 60f)] public float DamageBoosterDuration;
    
    [Header("Weapon Booster")] 
    public float ReducingAttackDelay;
    public float ReducingReloadDelay;
    [Range(10f, 60f)] public float WeaponBoosterDuration;
}