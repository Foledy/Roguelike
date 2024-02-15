﻿using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public static class Utils
{
    public static List<Collider> GetAllColliders(this GameObject go) =>
            go == null ? null : go.GetComponents<Collider>().ToList();

    public static void ToWorldSpaceBox(this BoxCollider box, out float3 center,
        out float3 halfExtents, out quaternion orientation)
    {
        var transform = box.transform;
        orientation = transform.rotation;
        center = transform.TransformPoint(box.center);
        var lossyScale = transform.lossyScale;
        var scale = Abs(lossyScale);
        halfExtents = Vector3.Scale(scale, box.size) * 0.5f;
    }

    public static void ToWorldSpaceCapsule(this CapsuleCollider capsule, out float3 point0,
        out float3 point1, out float radius)
    {
        var transform = capsule.transform;
        var center = (float3)transform.TransformPoint(capsule.center);
        radius = 0f;
        float height = 0f;
        float3 lossyScale = Abs(transform.lossyScale);
        float3 dir = float3.zero;

        switch (capsule.direction)
        {
            case 0:
                radius = Mathf.Max(lossyScale.y, lossyScale.z) * capsule.radius;
                height = lossyScale.x * capsule.height;
                dir = capsule.transform.TransformDirection(Vector3.right);
                break;
            case 1:
                radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsule.radius;
                height = lossyScale.x * capsule.height;
                dir = capsule.transform.TransformDirection(Vector3.up);
                break;
            case 2:
                radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsule.radius;
                height = lossyScale.z * capsule.height;
                dir = capsule.transform.TransformDirection(Vector3.forward);
                break;
        }

        if (height < radius * 2f)
        {
            dir = Vector3.zero;
        }

        point0 = center + dir * (height * 0.5f - radius);
        point1 = center - dir * (height * 0.5f - radius);
    }

    public static void ToWorldSpaceSphere(this SphereCollider sphere, out float3 center, out float radius)
    {
        var transform = sphere.transform;
        center = transform.TransformPoint(sphere.center);
        radius = sphere.radius * Max(Abs(transform.lossyScale));
    }

    public static float3 Abs(float3 v) => new float3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));

    public static float Max(float3 v) => Mathf.Max(v.x, Mathf.Max(v.y, v.z));

    public static IBooster UpdateBooster(this IBooster booster, float deltaTime)
    {
        if (booster.IsActive == true)
        {
            if (booster.Duration > 0)
            {
                booster.Duration -= deltaTime;
            }
            else
            {
                booster.Duration = 0;
                booster.IsActive = false;
            }

            return booster;
        }

        return null;
    }
}