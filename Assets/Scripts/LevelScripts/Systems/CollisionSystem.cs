﻿using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;

    private Collider[] _results = new Collider[32];
    
    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), ComponentType.ReadOnly<CollisionAbility>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_collisionQuery).ForEach(
            (Entity entity, CollisionAbility abilityCollision, ref ActorColliderData colliderData) =>
                {
                    if (abilityCollision != null)
                    {
                        var gameObject = abilityCollision.gameObject;
                        float3 position = gameObject.transform.position;
                        Quaternion rotation = gameObject.transform.rotation;
                        
                        abilityCollision.Colliders?.Clear();

                        int size = 0;

                        switch (colliderData.ColliderType)
                        {
                            case ColliderType.Sphere:
                                size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position,
                                    colliderData.SphereRadius, _results);
                                break;
                            
                            case ColliderType.Capsule:
                                var center =
                                    ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                                var point1 = colliderData.CapsuleStart + position;
                                var point2 = colliderData.CapsuleEnd + position;
                                point1 = (float3)(rotation * (point1 - center)) + center;
                                point2 = (float3)(rotation * (point2 - center)) + center;
                                size = Physics.OverlapCapsuleNonAlloc(point1, point2,
                                    colliderData.CapsuleRadius, _results);
                                break;
                            
                            case ColliderType.Box:
                                size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position,
                                    colliderData.BoxHalfExtents, _results, colliderData.BoxOrientation * rotation);
                                break;
                            
                            default:
                                throw new ArgumentException();
                        }

                        if (size > 0)
                        {
                            var colliders = new List<Collider>();
                            
                            foreach (var collider in _results)
                            {
                                if(collider != null)
                                    colliders.Add(collider);
                            }

                            abilityCollision.Colliders = colliders;
                            
                            abilityCollision.Execute();
                        }
                    }
                });
    }
}