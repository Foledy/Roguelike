using System.Collections.Generic;
using UnityEngine;

public class PickupAbility : MonoBehaviour, IAbilityTarget
{
    public List<Collider> Colliders { get; set; }
    
    public void Execute()
    {
        
    }
}