using Unity.Entities;
using UnityEngine;

public class CharacterSpeedSystem : ComponentSystem
{
    private EntityQuery _speedQuery;

    protected override void OnCreate()
    {
        _speedQuery = GetEntityQuery(ComponentType.ReadOnly<Character>(), ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<BoosterData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_speedQuery).ForEach(
            (Entity entity, Character character, ref MoveData moveData, ref BoosterData boosterData) =>
            {
                if (boosterData.SpeedBooster.IsActive == true)
                {
                    moveData.MoveSpeed = character.CharacterSettings.MoveSpeed / 100 * boosterData.SpeedBooster.Multiplier;
                }
                else
                {
                    moveData.MoveSpeed = character.CharacterSettings.MoveSpeed / 100;
                }
            });
    }
}