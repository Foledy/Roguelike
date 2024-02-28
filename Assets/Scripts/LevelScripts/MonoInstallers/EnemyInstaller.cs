using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private GameObject _weekEnemyPrefab;
    [SerializeField] private GameObject _giantEnemyPrefab;
    [SerializeField] private GameObject _shooterEnemyPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<EnemyPoolService>().AsSingle().NonLazy();

        Container.BindMemoryPool<WeekEnemy, WeekEnemy.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_weekEnemyPrefab)
            .UnderTransformGroup("WeekEnemies");
        Container.BindMemoryPool<GiantEnemy, GiantEnemy.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_giantEnemyPrefab)
            .UnderTransformGroup("GiantEnemies");
        Container.BindMemoryPool<ShooterEnemy, ShooterEnemy.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_shooterEnemyPrefab)
            .UnderTransformGroup("ShooterEnemies");
    }
}