using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Create new character", order = 0)]
public class CharacterSettings : ScriptableObject
{
    [field: SerializeField] public GameObject CharacterPrefab { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float Protection { get; private set; }
}