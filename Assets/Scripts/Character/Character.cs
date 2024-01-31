using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    public CharacterSettings CharacterSettings { get; private set; }
    [Inject] public BoostersSettings BoostersSettings { get; private set; }
}