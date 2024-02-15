using System.Collections;
using IJunior.TypedScenes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingEntry : MonoBehaviour, ISceneLoadHandler<CharacterSettings>
{
    private AsyncOperation _operation;
    private TMP_Text _text;
    
    public void OnSceneLoaded(CharacterSettings character)
    {
        _text = GetComponent<TMP_Text>();

        _operation = SceneManager.LoadSceneAsync("LevelScene");
        _operation.allowSceneActivation = false;
        _operation.completed += _ =>
        {
            foreach (var handler in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (handler.TryGetComponent(out ISceneLoadHandler<CharacterSettings> entry) == true)
                {
                    entry.OnSceneLoaded(character);
                }
            }
        };
        
        StartCoroutine(LoadingRoutine());
    }
    
    private IEnumerator LoadingRoutine()
    {
        var progress = 0f;
        
        while (progress < 89)
        {
            progress = progress + 1f <= _operation.progress * 100 ? progress + 1f : progress;
            
            _text.text = progress + "%";
            
            yield return new WaitForSeconds(.02f);
        }

        while (progress < 100)
        {
            progress += 1f;
            
            _text.text = progress + "%";
            
            yield return new WaitForSeconds(.01f);
        }

        _operation.allowSceneActivation = true;
    }
}