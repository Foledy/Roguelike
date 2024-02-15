using System;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private static CharacterSettings _characterSettings;

    public void OnSelectPlayerClicked(CharacterSettings settings) => _characterSettings = settings;
    
    public void OnPlayClicked() => LoadingScene.Load(_characterSettings);

    public void OnQuitClicked() => Application.Quit();
}