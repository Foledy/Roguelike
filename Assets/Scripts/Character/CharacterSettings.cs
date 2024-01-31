using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Create new character", order = 0)]
public class CharacterSettings : ScriptableObject
{
    [field: SerializeField] public GameObject CharacterPrefab { get; private set; }
    [field: SerializeField, Range(75, 300)] public float Health { get; private set; }
    [field: SerializeField, Range(2, 10)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Range(0.5f, 1.5f)] public float AttackSpeed { get; private set; }
    [field: SerializeField, Range(0.75f, 1.5f)] public float Protection { get; private set; }
}