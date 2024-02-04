using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponBoosterSettings", menuName = "Create Booster/Create WeaponBooster Settings", order = 0)]
public class WeaponBoosterSettings : ScriptableObject
{
    [Range(0, 1f)] public float ReducingAttackDelay;
    [Range(0, 1f)] public float ReducingReloadDelay;
    [Range(10f, 60f)] public float WeaponBoosterDuration;
}