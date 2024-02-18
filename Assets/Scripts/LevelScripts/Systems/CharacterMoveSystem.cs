using Unity.Entities;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<FirstPersonCamera>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, FirstPersonCamera character, ref MoveData moveData, ref InputData input) =>
            {
                character.Move(input, moveData);
                character.MouseLook(input.Rotation);
            });
    }
}