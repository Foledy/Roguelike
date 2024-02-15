using IJunior.TypedScenes;
using TMPro;
using UnityEngine;

public class ResultEntry : MonoBehaviour, ISceneLoadHandler<Result>
{
    [SerializeField] private TMP_Text _weekKilled;
    [SerializeField] private TMP_Text _giantKilled;
    [SerializeField] private TMP_Text _shooterKilled;
    [SerializeField] private TMP_Text _livedSeconds;
    
    public void OnSceneLoaded(Result result)
    {
        _livedSeconds.text = $"{result.LivedSeconds}";
        _weekKilled.text = $"{result.WeekKilled}";
        _giantKilled.text = $"{result.GiantKilled}";
        _shooterKilled.text = $"{result.ShooterKilled}";
    }
}
