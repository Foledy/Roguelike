using Unity.Entities;
using UnityEngine;

public class EnemyAISystem : ComponentSystem
{
    private EntityQuery _enemyQuery;
    private EntityQuery _targetQuery;

    protected override void OnCreate()
    {
        _enemyQuery = GetEntityQuery(ComponentType.ReadOnly<Enemy>(), ComponentType.ReadOnly<EnemyData>());
        _targetQuery = GetEntityQuery(ComponentType.ReadOnly<Character>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_enemyQuery).ForEach(
            (Entity entity, Enemy enemy, ref EnemyData data) =>
            {
                if (enemy.enabled == false)
                {
                    return;
                }

                var enemyPos = enemy.transform.position;
                var target = GetTargetPosition(enemyPos);
                
                if (target == enemyPos * 100)
                {
                    return;
                }
                
                enemy.SetTarget(target);
            });
    }
    
    private Vector3 GetTargetPosition(Vector3 source)
    {
        Vector3 target = source * 100;

        Entities.With(_targetQuery).ForEach(
            (Entity entity, Character character) =>
            {
                var position = character.transform.position;

                if (Vector3.Distance(source, position) < Vector3.Distance(source, target))
                {
                    target = position;
                }
            });

        return target;
    }
}