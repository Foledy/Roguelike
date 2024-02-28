using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(HealthAbility))]
public class Enemy : MonoBehaviour, IConvertGameObjectToEntity
{
    private event Action OnKilled;
    
    [SerializeField] private float _stoppingDistance;
    
    private NavMeshAgent _agent;
    private HealthAbility _health;
    private Transform _currentTarget;
    private List<Action> _subscribers;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _health = GetComponent<HealthAbility>();
        _agent.stoppingDistance = _stoppingDistance;
        _subscribers = new List<Action>();
    }

    private void OnEnable()
    {
        _health.OnDead += OnDead;
    }

    private void OnDisable()
    {
        _health.OnDead -= OnDead;
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

    public void Subcribe(Action callback)
    {
        _subscribers.Add(callback);

        OnKilled += callback;
    }

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
    }

    private void OnDead()
    {
        OnKilled?.Invoke();

        UnSubscribeAll();
    }

    private void UnSubscribeAll()
    {
        foreach (var sub in _subscribers)
        {
            OnKilled -= sub;
        }

        _subscribers.Clear();
    }
}

public struct EnemyData : IComponentData
{
    
}