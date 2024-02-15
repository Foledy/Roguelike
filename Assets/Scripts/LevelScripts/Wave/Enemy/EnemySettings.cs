using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy Settings", menuName = "Roguelike/Create Enemy Settings", order = 0)]
public class EnemySettings : ScriptableObject
{
    public GameObject EnemyPrefab;
    public EnemyType EnemyType;
    public float Speed;
    public float Damage;
}