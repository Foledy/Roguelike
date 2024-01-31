using Unity.Entities;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<Rigidbody>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, Rigidbody rigidbody, ref MoveData moveData, ref InputData input) =>
            {
                if (input.Move.x != 0 || input.Move.y != 0)
                {
                    var velocity = new Vector3(input.Move.x * moveData.MoveSpeed, rigidbody.velocity.y, input.Move.y * moveData.MoveSpeed);

                    if (input.Sprint == 1)
                    {
                        velocity.x *= moveData.SprintBoost;
                        velocity.z *= moveData.SprintBoost;
                    }
                    
                    rigidbody.velocity = velocity;
                    
                    var transform = rigidbody.transform;
                    
                    var dir = new Vector3(input.Move.x, 0, input.Move.y);
                    
                    if (dir == Vector3.zero)
                    {
                        return;
                    }

                    var rot = transform.rotation;
                    var newRot = Quaternion.LookRotation(Vector3.Normalize(dir));
                    
                    if (newRot == rot)
                    {
                        return;
                    }

                    transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime*moveData.RotationSpeed);
                }
            });
    }
}