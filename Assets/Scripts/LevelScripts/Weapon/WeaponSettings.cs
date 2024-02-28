using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Settings", menuName = "Roguelike/Create new weapon", order = 0)]
public class WeaponSettings : ScriptableObject
{
    [Header("Main Settings")]
    public GameObject WeaponPrefab;
    public AudioClip AttackClip;
    public Vector2 AudioPitch = new Vector2(.9f, 1.1f);
    public GameObject MuzzlePrefab;

    [Header("Weapon Parameters")] 
    public float Damage;
    public float AttackDelay;
    public float AttackDistance;
    public bool IsShootingWeapon;

    [Header("Projectile")] 
    public GameObject EnemyProjectilePrefab;
    public GameObject EnvironmentProjectilePrefab;
    
    public float ReloadDelay { get; set; }
    public int AmmoAmount { get; set; }
}