using UnityEngine;
using Zenject;

public class WaveInstaller : MonoInstaller
{
    [SerializeField] private WaveDifficultSettings _difficultSettings;
    [SerializeField] private WaveSettings _waveSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<WaveDifficultSettings>().FromInstance(_difficultSettings).AsSingle().NonLazy();
        Container.Bind<WaveSettings>().FromInstance(_waveSettings).AsSingle().NonLazy();
    }
}