using System;

[Serializable]
public class BoostersParameters
{
    public HealthParameters Health { get; }
    public SpeedParameters Speed { get; }
    public ProtectionParameters Protection { get; }
    public WeaponParameters Weapon { get; }
    public DamageParameters Damage { get; }

    public BoostersParameters(HealthParameters health, SpeedParameters speed,
        ProtectionParameters protection, WeaponParameters weapon, DamageParameters damage)
    {
        Health = health;
        Speed = speed;
        Protection = protection;
        Damage = damage;
        Weapon = weapon;
    }
}