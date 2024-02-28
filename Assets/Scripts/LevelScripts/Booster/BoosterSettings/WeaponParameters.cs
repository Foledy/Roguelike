using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new WeaponBoosterSettings", menuName = "Roguelike/Create Booster/Create WeaponBooster Settings", order = 0)]
public class WeaponParameters : ScriptableObject
{
    [Range(1f, 3f)] public float DelayDivider;
    [FormerlySerializedAs("WeaponBoosterDuration")] [Range(10f, 60f)] public float Duration;
}