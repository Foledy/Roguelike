using Unity.Entities;

public class CharacterAttackSystem : ComponentSystem
{
    private EntityQuery _attackQuery;

    protected override void OnCreate()
    {
        _attackQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<AttackAbility>(), ComponentType.ReadOnly<BoosterData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_attackQuery).ForEach(
            (Entity entity, AttackAbility attack, ref BoosterData boosterData, ref InputData input) =>
            {
                if (input.Attack == 1)
                {
                    attack.Attack(boosterData.Damage, boosterData.Weapon);
                }
            });
    }
}