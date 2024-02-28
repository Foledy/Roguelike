using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new DamageBoosterSettings", menuName = "Roguelike/Create Booster/Create DamageBooster Settings", order = 0)]
public class DamageParameters : ScriptableObject
{
    [FormerlySerializedAs("DamageBoosterDuration")] [Range(10f, 60f)] public float Duration;
    [Range(1f, 3f)] public float DamageMultiplier;
}