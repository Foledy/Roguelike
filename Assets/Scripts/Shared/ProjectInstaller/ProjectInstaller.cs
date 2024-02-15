using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IDataService>().FromInstance(new DataService()).AsSingle().NonLazy();
        Container.Bind<IAudioService>().FromInstance(new AudioService()).AsSingle().NonLazy();
    }
}
