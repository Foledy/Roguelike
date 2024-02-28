using UnityEngine;
using Zenject;

public class BoosterInstaller : MonoInstaller
{
    [SerializeField] private BoosterDropSettings _dropSettings;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _speed;
    [SerializeField] private GameObject _protection;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _damage;
    
    public override void InstallBindings()
    {
        Container.Bind<BoosterDropSettings>().FromInstance(_dropSettings).AsSingle().NonLazy();
        Container.Bind<BoosterPoolService>().AsSingle();

        Container.BindMemoryPool<HealthBooster, HealthBooster.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_health)
            .UnderTransformGroup("Boosters");
        Container.BindMemoryPool<SpeedBooster, SpeedBooster.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_speed)
            .UnderTransformGroup("Boosters");
        Container.BindMemoryPool<ProtectionBooster, ProtectionBooster.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_protection)
            .UnderTransformGroup("Boosters");
        Container.BindMemoryPool<WeaponBooster, WeaponBooster.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_weapon)
            .UnderTransformGroup("Boosters");
        Container.BindMemoryPool<DamageBooster, DamageBooster.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_damage)
            .UnderTransformGroup("Boosters");
    }
}