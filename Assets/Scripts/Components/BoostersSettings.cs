using UnityEngine;

[CreateAssetMenu(fileName = "Boosters Settings", menuName = "Create new boosters settings", order = 0)]
public class BoostersSettings : ScriptableObject
{
    [Header("Health Booster")] 
    public float HealthRecovery;

    [Header("Protection Booster")] 
    public float AdditionalProtection;

    [Header("Speed Booster")] 
    public float AdditionalSpeed;

    [Header("Damage Booster")] 
    public float AdditionalDamage;
    
    [Header("Weapon Booster")] 
    public float ReducingAttackDelay;
    public float ReducingReloadDelay;
}