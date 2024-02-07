using System;
using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
public class HealthAbility : MonoBehaviour
{
    public float Health { get; private set; }

    public event Action OnHealthChanged;

    public void TakeDamage(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("[Health Ability] Damage can`t be lower than 0!");
        }

        Health -= value;
        
        OnHealthChanged?.Invoke();
    }

    public void Heal(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("[Health Ability] Heal value can`t be lower than 0!");
        }

        Health += value;
        
        OnHealthChanged?.Invoke();
    }
}