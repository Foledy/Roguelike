using Unity.Entities;

public class RotationSystem : ComponentSystem
{
    private EntityQuery _rotationQuery;

    protected override void OnCreate()
    {
        _rotationQuery = GetEntityQuery(ComponentType.ReadOnly<CameraRotator>(), 
            ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_rotationQuery).ForEach(
            (Entity entity, CameraRotator rotator, ref InputData input) =>
            {
                rotator.Rotate(input.Rotation);
            });
    }
}