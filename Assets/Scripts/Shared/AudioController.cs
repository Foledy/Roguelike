using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(AudioListener))]
public class AudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _vfxSource;
    [SerializeField] private AudioSource _musicSource;

    [Header("Sliders")] 
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _vfxSlider;
    
    [Inject] private IAudioService _audioService;

    private void Start()
    {
        _musicSlider.value = _audioService.GetMusicVolume();
        _vfxSlider.value = _audioService.GetVfxVolume();
    }

    public void SetMusicVolume(Slider slider)
    {
        _audioService.SetMusicVolume(slider.value);

        _musicSource.volume = slider.value;
    }

    public void SetVfxVolume(Slider slider)
    {
        _audioService.SetVfxVolume(slider.value);

        _vfxSource.volume = slider.value;
    }

    [Inject]
    private void Init(IDataService dataService)
    {
        _audioService.Init(dataService);
    }
}