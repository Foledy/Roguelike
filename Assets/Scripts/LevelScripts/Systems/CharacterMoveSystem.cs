using Unity.Entities;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, Transform transform, ref MoveData moveData, ref InputData input) =>
            {
                if (input.Move.x != 0 || input.Move.y != 0)
                {
                    var pos = transform.position;
                    pos += new Vector3(input.Move.x * moveData.MoveSpeed, 0, input.Move.y * moveData.MoveSpeed);
                    transform.position = pos;
                    
                    var direction = new Vector3(input.Move.x, 0, input.Move.y);
                    
                    if (direction == Vector3.zero)
                    {
                        return;
                    }

                    var rot = transform.rotation;
                    var newRot = Quaternion.LookRotation(Vector3.Normalize(direction));
                    
                    if (newRot == rot)
                    {
                        return;
                    }

                    transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime*moveData.RotationSpeed);
                }
            });
    }
}