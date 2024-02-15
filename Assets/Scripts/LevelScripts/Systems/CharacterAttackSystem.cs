using Unity.Entities;
using UnityEngine;

public class CharacterAttackSystem : ComponentSystem
{
    private EntityQuery _attackQuery;

    protected override void OnCreate()
    {
        _attackQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(),ComponentType.ReadOnly<AttackAbility>(),
            ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<BoosterData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_attackQuery).ForEach(
            (Entity entity, AttackAbility attack, ref BoosterData boosterData, ref InputData input) =>
            {
                if (input.Attack == 1)
                {
                    attack.Attack(boosterData.DamageBooster, boosterData.WeaponBooster);
                }
            });
    }
}