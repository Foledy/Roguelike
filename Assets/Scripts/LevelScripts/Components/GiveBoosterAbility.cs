using System.Collections.Generic;
using UnityEngine;

public class GiveBoosterAbility : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private BoosterType _boosterType;
    public List<Collider> Colliders { get; set; }

    public void Execute()
    {
        foreach (var collider in Colliders)
        {
            if (collider.TryGetComponent(out Character character) == true)
            {
                character.AddBoosterToQueue(_boosterType);

                break;
            }
        }
    }
}