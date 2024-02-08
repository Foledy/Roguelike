using Unity.Entities;

public class EnemyDifficultySystem : ComponentSystem
{
    private EntityQuery _enemyQuery;

    protected override void OnCreate()
    {
        _enemyQuery = GetEntityQuery(ComponentType.ReadOnly<IEnemySpawner>(), ComponentType.ReadOnly<EnemySpawnData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_enemyQuery).ForEach(
            (Entity entity, IEnemySpawner enemySpawner, ref EnemySpawnData spawnData) =>
            {
                if (spawnData.SpawnDataChanged == false)
                {
                    spawnData.SpawnDataChanged = true;

                    spawnData.WeakMeleeData.Amount += enemySpawner.EnemySpawnerInfo.WeakIncreaseAmount;
                    spawnData.GiantMeleeData.Amount += enemySpawner.EnemySpawnerInfo.GiantIncreaseAmount;
                    spawnData.ShooterData.Amount += enemySpawner.EnemySpawnerInfo.ShooterIncreaseAmount;
                }
            });
    }
}