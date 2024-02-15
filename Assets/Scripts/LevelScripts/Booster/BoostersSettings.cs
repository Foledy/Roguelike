public class BoostersSettings
{
    public HealthBoosterSettings HealthBoosterSettings { get; }
    public SpeedBoosterSettings SpeedBoosterSettings { get; }
    public ProtectionBoosterSettings ProtectionBoosterSettings { get; }
    public WeaponBoosterSettings WeaponBoosterSettings { get; }
    public DamageBoosterSettings DamageBoosterSettings { get; }

    public BoostersSettings(HealthBoosterSettings health, SpeedBoosterSettings speed,
        ProtectionBoosterSettings protection, WeaponBoosterSettings weapon, DamageBoosterSettings damage)
    {
        HealthBoosterSettings = health;
        SpeedBoosterSettings = speed;
        ProtectionBoosterSettings = protection;
        DamageBoosterSettings = damage;
        WeaponBoosterSettings = weapon;
    }
}