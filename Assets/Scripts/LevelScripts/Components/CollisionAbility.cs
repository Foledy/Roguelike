using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private List<MonoBehaviour> _collisionActions;
    [SerializeField] private Collider _collider;

    public List<Collider> Colliders { get; set; }
    
    private List<IAbility> _abilityActions;

    private void Start()
    {
        _abilityActions = new List<IAbility>();

        foreach (var action in _collisionActions)
        {
            if (action is IAbility ability)
                _abilityActions.Add(ability);
            else
                throw new ArgumentException("[Collision Ability] Action must be IAbility!");
        }
    }

    public void Execute()
    {
        foreach (var action in _abilityActions)
        {
            if (action is IAbilityTarget targetAction)
            {
                targetAction.Colliders = Colliders;
            }
            
            action.Execute();
        }
    }
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        
        switch (_collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType =  ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    InitialTakeOff = true
                });
                
                break;
            
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    InitialTakeOff = true
                });
                
                break;
            
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    InitialTakeOff = true
                });
                
                break;
        }
    }
}

public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool InitialTakeOff;
}

public enum ColliderType
{
    Sphere = 0,
    Capsule = 1,
    Box = 2
}