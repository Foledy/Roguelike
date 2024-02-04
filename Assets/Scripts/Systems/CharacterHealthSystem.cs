using System.Collections.Generic;
using Unity.Entities;

public class CharacterHealthSystem : ComponentSystem
{
    private EntityQuery _healthQuery;

    protected override void OnCreate()
    {
        _healthQuery = GetEntityQuery(ComponentType.ReadOnly<HealthAbility>(), ComponentType.ReadOnly<HealthHandler>(),
            ComponentType.ReadOnly<BoosterData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_healthQuery).ForEach(
            (Entity entity, HealthAbility health, ref BoosterData boosterData) =>
            {
                var handler = health.GetComponent<HealthHandler>();
                
                if (handler.TryGetAction(out KeyValuePair<HealthActionType, float> action) == true)
                {
                    if (action.Key != HealthActionType.None)
                    {
                        var character = health.GetComponent<Character>();
                        
                        if (action.Key == HealthActionType.Heal)
                        {
                            health.Heal(action.Value);
                        }
                        else
                        {
                            var damage = action.Value;

                            var protection = character.CharacterSettings.Protection;
                            
                            if (boosterData.ProtectionBooster.IsActive == true)
                            {
                                protection *= boosterData.ProtectionBooster.Multiplier;
                            }

                            damage /= protection;
                            
                            health.TakeDamage(damage);
                        }
                    }
                }
            });
    }
}