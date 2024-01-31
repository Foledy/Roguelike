using Unity.Entities;
using UnityEngine;

public class InputData : MonoBehaviour, IConvertGameObjectToEntity
{
    private float _speed;
    
    [Inject]
    private void Construct(float speed)
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        
    }
}