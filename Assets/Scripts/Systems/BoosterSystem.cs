using Unity.Entities;

public class BoosterSystem : ComponentSystem
{
    private EntityQuery _boosterQuery;

    protected override void OnCreate()
    {
        _boosterQuery = GetEntityQuery(ComponentType.ReadOnly<BoosterData>());
    }

    protected override void OnUpdate()
    {
        
    }
}