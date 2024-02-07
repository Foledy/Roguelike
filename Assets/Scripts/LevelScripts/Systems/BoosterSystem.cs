using Unity.Entities;
using UnityEngine;

public class BoosterSystem : ComponentSystem
{
    private EntityQuery _boosterQuery;

    protected override void OnCreate()
    {
        _boosterQuery = GetEntityQuery(ComponentType.ReadOnly<Character>(), ComponentType.ReadOnly<BoosterData>());
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        
        Entities.With(_boosterQuery).ForEach(
            (Entity entity, Character character, ref BoosterData boosterData) =>
            {
                if (character.TryGetBoosterFromQueue(out BoosterType type) == true)
                {
                    switch (type)
                    {
                        case BoosterType.Health:
                            character.GetComponent<HealthHandler>().AddActionToQueue(HealthActionType.Heal,
                                character.BoostersSettings.HealthBoosterSettings.HealthRecovery);
                            break;
                        case BoosterType.Speed:
                            var speedBooster = boosterData.SpeedBooster;
                            
                            speedBooster.Duration += character.BoostersSettings
                                .SpeedBoosterSettings.SpeedBoosterDuration;
                            speedBooster.Multiplier = character.BoostersSettings.SpeedBoosterSettings.SpeedMultiplier;
                            speedBooster.IsActive = true;

                            boosterData.SpeedBooster = speedBooster;
                            
                            break;
                        case BoosterType.Protection:
                            var protectionBooster = boosterData.ProtectionBooster;

                            protectionBooster.Duration += character.BoostersSettings
                                .ProtectionBoosterSettings.ProtectionBoosterDuration;
                            protectionBooster.Multiplier = character.BoostersSettings
                                .ProtectionBoosterSettings.ProtectionMultiplier;
                            protectionBooster.IsActive = true;
                            
                            break;
                        case BoosterType.Weapon:
                            var weaponBooster = boosterData.WeaponBooster;

                            weaponBooster.Duration += character.BoostersSettings.WeaponBoosterSettings
                                .WeaponBoosterDuration;
                            weaponBooster.ReducingAttackDelay =
                                character.BoostersSettings.WeaponBoosterSettings.ReducingAttackDelay;
                            weaponBooster.ReducingReloadDelay =
                                character.BoostersSettings.WeaponBoosterSettings.ReducingReloadDelay;
                            weaponBooster.IsActive = true;
                            
                            break;
                        case BoosterType.Damage:
                            var damageBooster = boosterData.DamageBooster;

                            damageBooster.Duration += character.BoostersSettings.DamageBoosterSettings
                                .DamageBoosterDuration;
                            damageBooster.Multiplier =
                                character.BoostersSettings.DamageBoosterSettings.DamageMultiplier;
                            damageBooster.IsActive = true;
                            
                            break;
                    }
                }

                UpdateBoostersDuration(ref boosterData, deltaTime);
            });
    }

    private void UpdateBoostersDuration(ref BoosterData boosterData, float deltaTime)
    {
        var speed = boosterData.SpeedBooster.UpdateBooster(deltaTime);
        var protection = boosterData.ProtectionBooster.UpdateBooster(deltaTime);
        var weapon = boosterData.WeaponBooster.UpdateBooster(deltaTime);
        var damage = boosterData.DamageBooster.UpdateBooster(deltaTime);

        if (speed != null)
        {
            var booster = boosterData.SpeedBooster;
            
            booster.Duration = speed.Duration;
            booster.IsActive = speed.IsActive;

            boosterData.SpeedBooster = booster;
        }

        if (protection != null)
        {
            var booster = boosterData.ProtectionBooster;
            
            booster.Duration = protection.Duration;
            booster.IsActive = protection.IsActive;

            boosterData.ProtectionBooster = booster;
        }

        if (weapon != null)
        {
            var booster = boosterData.WeaponBooster;
            
            booster.Duration = weapon.Duration;
            booster.IsActive = weapon.IsActive;

            boosterData.WeaponBooster = booster;
        }

        if (damage != null)
        {
            var booster = boosterData.DamageBooster;
            
            booster.Duration = damage.Duration;
            booster.IsActive = damage.IsActive;

            boosterData.DamageBooster = booster;
        }
    }
}