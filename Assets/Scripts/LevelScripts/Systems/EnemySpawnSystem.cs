using Unity.Entities;

public class EnemySpawnSystem : ComponentSystem
{
    private EntityQuery _spawnQuery;

    protected override void OnCreate()
    {
        _spawnQuery = GetEntityQuery(ComponentType.ReadOnly<IEnemySpawner>(),
            ComponentType.ReadOnly<EnemySpawnData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_spawnQuery).ForEach(
            (Entity entity, IEnemySpawner enemySpawner, ref EnemySpawnData spawnData) =>
            {
                if (enemySpawner.NeedSpawnEnemies == true)
                {
                    enemySpawner.SpawnEnemies(spawnData);
                }
            });
    }
}