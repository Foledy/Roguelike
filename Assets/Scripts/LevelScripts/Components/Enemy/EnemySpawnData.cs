using Unity.Entities;

public struct EnemySpawnData : IComponentData
{
    public int Wave;
    public bool SpawnDataChanged;
    public EnemyData WeakMeleeData;
    public EnemyData GiantMeleeData;
    public EnemyData ShooterData;
}