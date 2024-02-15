using IJunior.TypedScenes;
using UnityEngine;

public class LevelEntry : MonoBehaviour, ISceneLoadHandler<CharacterSettings>
{
    [SerializeField] private Transform _playerSpawn;
    
    public void OnSceneLoaded(CharacterSettings character)
    {
        SpawnPlayer(character.CharacterPrefab);
        
        
    }

    private void SpawnPlayer(GameObject prefab) => Instantiate(prefab, _playerSpawn.position, Quaternion.identity);
}