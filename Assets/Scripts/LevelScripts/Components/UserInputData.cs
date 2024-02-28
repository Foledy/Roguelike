using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    private Entity _entity;
    private EntityManager _dstManager;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        
        dstManager.AddComponentData(entity, new MoveData{ MoveSpeed = 12f, SprintBoost = 1.5f });

        _entity = entity;
        _dstManager = dstManager;
    }

    private void OnDestroy()
    {
        _dstManager.DestroyEntity(_entity);
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public Vector2 Rotation;
    public float Jump;
    public float Sprint;
    public float Attack;
    public float Reload;
}

public struct MoveData : IComponentData
{
    public float MoveSpeed;
    public float SprintBoost;
}

public struct AnimationData : IComponentData
{
    
}