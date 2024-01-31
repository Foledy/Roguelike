using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{ 
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData());
        dstManager.AddComponentData(entity, new BoosterData());
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Sprint;
    public float Attack;
    public float Reload;
}

public struct MoveData : IComponentData
{
    public float MoveSpeed;
    public float SprintBoost;
    public float RotationSpeed;
}

public struct AnimationData : IComponentData
{
    
}

public struct BoosterData : IComponentData
{
    public bool Health;
    public bool Protection;
    public bool Speed;
    public bool Damage;
    public bool Weapon;
}