using System.Collections.Generic;
using UnityEngine;

public interface IAbilityTarget : IAbility
{
    List<Collider> Colliders { get; set; }
}