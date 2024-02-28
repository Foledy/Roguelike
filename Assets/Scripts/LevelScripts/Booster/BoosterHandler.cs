using NaughtyAttributes;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BoosterHandler : MonoBehaviour
{
    [Inject] private BoosterDropSettings _dropSettings;
    [Inject] private BoosterPoolService _boosterService;
    [Inject] private EnemyPoolService _enemyService;
    private Character _character;

    private void OnEnable()
    {
        _enemyService.OnEnemyWithBoosterKilled += OnEnemyKilled;
        _boosterService.OnPickedUp += OnPickedUp;
    }

    private void OnDisable()
    {
        _enemyService.OnEnemyWithBoosterKilled -= OnEnemyKilled;
        _boosterService.OnPickedUp -= OnPickedUp;
    }

    public void BindCharacter(Character character) => _character = character;
    
    // При расширении можно добавить множитель шанса в зависимости от моба
    private void OnEnemyKilled(Vector3 position)
    {
        var booster = GetRandomBooster();

        if (TryLuck(_dropSettings.GetChance(booster)) == true)
        {
            _boosterService.Spawn(booster, position, Quaternion.identity);
        }
    }

    private bool TryLuck(float chance) => Random.Range(0f, 100f) <= chance;

    private BoosterType GetRandomBooster()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return BoosterType.Health;
            case 2:
                return BoosterType.Speed;
            case 3:
                return BoosterType.Protection;
            case 4:
                return BoosterType.Weapon;
            case 5:
                return BoosterType.Damage;
            
            default:
                throw new System.ArgumentException("[Booster Handler] Wrong booster index!");
        }
    }

    private void OnPickedUp(BoosterType type)
    {
        _character.AddBoosterToQueue(type);
    }
}