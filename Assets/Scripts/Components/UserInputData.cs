using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public CharacterSettings CharacterSettings { get; private set; }
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
    }

    [Inject]
    private void Construct(CharacterSettings characterSettings)
    {
        CharacterSettings = characterSettings;
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Attack;
}