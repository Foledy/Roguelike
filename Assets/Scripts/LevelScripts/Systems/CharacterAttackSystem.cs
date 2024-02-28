using Unity.Entities;
using UnityEngine;

public class CharacterAttackSystem : ComponentSystem
{
    private EntityQuery _attackQuery;

    protected override void OnCreate()
    {
        _attackQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(),ComponentType.ReadOnly<AttackAbility>(),
            ComponentType.ReadOnly<InputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_attackQuery).ForEach(
            (Entity entity, AttackAbility attack, ref InputData input) =>
            {
                if (input.Attack == 1)
                {
                    var character = attack.GetComponent<Character>();

                    var damageMultiplier = 1f;
                    var delayDivider = 1f;

                    if (character.BoosterData.Damage.IsActive == true)
                    {
                        damageMultiplier *= character.BoostersParameters.Damage.DamageMultiplier;
                    }

                    if (character.BoosterData.Weapon.IsActive == true)
                    {
                        delayDivider *= character.BoostersParameters.Weapon.DelayDivider;
                    }
                    
                    attack.Attack(damageMultiplier, delayDivider);
                }
            });
    }
}