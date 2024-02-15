using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [Header("Boosters")]
    [SerializeField] private HealthBoosterSettings _healthBoosterSettings;
    [SerializeField] private SpeedBoosterSettings _speedBoosterSettings;
    [SerializeField] private ProtectionBoosterSettings _protectionBoosterSettings;
    [SerializeField] private DamageBoosterSettings _damageBoosterSettings;
    [SerializeField] private WeaponBoosterSettings _weaponBoosterSettings;
    
    public override void InstallBindings()
    {
        var settings = new BoostersSettings(_healthBoosterSettings, _speedBoosterSettings, _protectionBoosterSettings,
            _weaponBoosterSettings, _damageBoosterSettings);

        Container.Bind<BoostersSettings>().FromInstance(settings).AsSingle().NonLazy();
    }
}
