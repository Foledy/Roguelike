using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{ 
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        
        dstManager.AddComponentData(entity, new MoveData{ MoveSpeed = 0.2f });
        dstManager.AddComponentData(entity, new BoosterData());
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public Vector2 Rotation;
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
    public SpeedBooster SpeedBooster { get; set; }
    public ProtectionBooster ProtectionBooster { get; set; }
    public WeaponBooster WeaponBooster { get; set; }
    public DamageBooster DamageBooster { get; set; }
}