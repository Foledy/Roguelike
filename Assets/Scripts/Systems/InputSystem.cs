using Unity.Entities;
using UnityEngine;

public class InputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<>())
    }
}
