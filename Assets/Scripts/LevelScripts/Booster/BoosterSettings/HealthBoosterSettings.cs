using UnityEngine;

[CreateAssetMenu(fileName = "new HealthBoosterSettings", menuName = "Roguelike/Create Booster/Create HealthBooster Settings", order = 0)]
public class HealthBoosterSettings : ScriptableObject
{
    public float HealthRecovery;
}