using IJunior.TypedScenes;
using UnityEngine;

public class LevelEntry : MonoBehaviour, ISceneLoadHandler<CharacterSettings>
{
    [SerializeField] private Transform _playerSpawn;
    
    public void OnSceneLoaded(CharacterSettings character)
    {
        SpawnPlayer(character);
    }

    private void SpawnPlayer(CharacterSettings character)
    {
        var player = Instantiate(character.CharacterPrefab, _playerSpawn.position, Quaternion.identity).GetComponent<Character>();
        
        player.SetCharacter(character);
    }
}