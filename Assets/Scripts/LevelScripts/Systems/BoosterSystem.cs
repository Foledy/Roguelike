using Unity.Entities;
using UnityEngine;

public class BoosterSystem : ComponentSystem
{
    private EntityQuery _boosterQuery;

    protected override void OnCreate()
    {
        _boosterQuery = GetEntityQuery(ComponentType.ReadOnly<Character>(), ComponentType.ReadOnly<UserInputData>());
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        
        Entities.With(_boosterQuery).ForEach(
            (Entity entity, Character character) =>
            {
                if (character.TryGetBoosterFromQueue(out BoosterType type) == true)
                {
                    switch (type)
                    {
                        case BoosterType.Health:
                            character.GetComponent<HealthHandler>().AddActionToQueue(HealthActionType.Heal,
                                character.BoostersParameters.Health.HealthRecovery);
                            
                            break;
                        case BoosterType.Speed:
                            character.BoosterData.ActivateBooster(
                                character.BoostersParameters.Speed.Duration, BoosterType.Speed);
                            
                            break;
                        case BoosterType.Protection:
                            character.BoosterData.ActivateBooster(
                                character.BoostersParameters.Protection.Duration, BoosterType.Protection);
                            
                            break;
                        case BoosterType.Weapon:
                            character.BoosterData.ActivateBooster(
                                character.BoostersParameters.Weapon.Duration, BoosterType.Weapon);
                           
                            break;
                        case BoosterType.Damage:
                            character.BoosterData.ActivateBooster(
                                character.BoostersParameters.Damage.Duration, BoosterType.Damage);
                            
                            break;
                    }
                }
                
                character.BoosterData.UpdateBoostersDuration(deltaTime);
            });
    }
}