using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private float _stoppingDistance;
    
    private NavMeshAgent _agent;
    private Transform _currentTarget;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.stoppingDistance = _stoppingDistance;
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EnemyData());
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        var enemyTransform = transform;
        
        enemyTransform.position = position;
        enemyTransform.rotation = rotation;
    }

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
    }
}

public struct EnemyData : IComponentData
{
    
}