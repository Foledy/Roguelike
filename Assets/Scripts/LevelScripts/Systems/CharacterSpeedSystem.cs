using Unity.Entities;
using UnityEngine;

public class CharacterSpeedSystem : ComponentSystem
{
    private EntityQuery _speedQuery;

    protected override void OnCreate()
    {
        _speedQuery = GetEntityQuery(ComponentType.ReadOnly<Character>(), ComponentType.ReadOnly<MoveData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_speedQuery).ForEach(
            (Entity entity, Character character, ref MoveData moveData) =>
            {
                moveData.MoveSpeed = character.CharacterSettings.MoveSpeed;
                
                if (character.BoosterData.Speed.IsActive)
                {
                    moveData.MoveSpeed *= character.BoostersParameters.Speed.SpeedMultiplier;
                }
            });
    }
}