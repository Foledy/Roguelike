using System.Collections.Generic;
using Unity.Entities;

public class EnemyHealthSystem : ComponentSystem
{
    private EntityQuery _entityQuery;

    protected override void OnCreate()
    {
        _entityQuery = GetEntityQuery(ComponentType.ReadOnly<Enemy>(), ComponentType.ReadOnly<HealthHandler>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_entityQuery).ForEach(
            (Entity entity, HealthHandler handler) =>
        {
            if (handler.TryGetAction(out var action) == true)
            {
                var health = handler.GetComponent<HealthAbility>();

                if (action.Key == HealthActionType.Heal)
                {
                    health.Heal(action.Value);
                }
                else
                {
                    health.TakeDamage(action.Value);
                }
            }
        });
    }
}