using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PickupAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbilityTarget
{
    public List<Collider> Colliders { get; set; }

    private EntityManager _dstManager;
    private Entity _entity;
    
    public void Execute()
    {
        foreach (var target in Colliders)
        {
            if (target.TryGetComponent(out Character character) == true)
            {
                Destroy(gameObject);
                _dstManager.DestroyEntity(_entity);

                break;
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dstManager = dstManager;
    }
}