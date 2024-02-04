using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [Header("Character")]
    [SerializeField] private CharacterSettings _characterSettings;
    
    [Header("Boosters")]
    [SerializeField] private HealthBoosterSettings _healthBoosterSettings;
    [SerializeField] private SpeedBoosterSettings _speedBoosterSettings;
    [SerializeField] private ProtectionBoosterSettings _protectionBoosterSettings;
    [SerializeField] private WeaponBoosterSettings _weaponBoosterSettings;
    [SerializeField] private DamageBoosterSettings _damageBoosterSettings;
    public override void InstallBindings()
    {
        var settings = new BoostersSettings(_healthBoosterSettings, _speedBoosterSettings, _protectionBoosterSettings,
            _weaponBoosterSettings, _damageBoosterSettings);

        Container.Bind<BoostersSettings>().FromInstance(settings).AsSingle().NonLazy();
        Container.Bind<CharacterSettings>().FromInstance(_characterSettings).AsTransient().NonLazy();
    }
}