using UnityEngine;

[CreateAssetMenu(fileName = "new DamageBoosterSettings", menuName = "Create Booster/Create DamageBooster Settings", order = 0)]
public class DamageBoosterSettings : ScriptableObject
{
    [Range(1f, 3f)] public float DamageMultiplier;
    [Range(10f, 60f)] public float DamageBoosterDuration;
}