using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponBoosterSettings", menuName = "Roguelike/Create Booster/Create WeaponBooster Settings", order = 0)]
public class WeaponBoosterSettings : ScriptableObject
{
    [Range(1.5f, 3)] public float ReducingAttackDelay;
    [Range(1.5f, 3f)] public float ReducingReloadDelay;
    [Range(10f, 60f)] public float WeaponBoosterDuration;
}