using UnityEngine;

[CreateAssetMenu(fileName = "new DamageBoosterSettings", menuName = "Roguelike/Create Booster/Create DamageBooster Settings", order = 0)]
public class DamageBoosterSettings : ScriptableObject
{
    [Range(10f, 60f)] public float DamageBoosterDuration;
    [Range(1f, 3f)] public float DamageMultiplier;
}