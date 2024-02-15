using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Settings", menuName = "Roguelike/Create new weapon", order = 0)]
public class WeaponSettings : ScriptableObject
{
    [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float AttackDistance { get; private set; }
    [field: SerializeField] public bool IsShootableWeapon { get; private set; }
    
    public int AmmoAmount { get; set; }
    public float ReloadDelay { get; set; }
}