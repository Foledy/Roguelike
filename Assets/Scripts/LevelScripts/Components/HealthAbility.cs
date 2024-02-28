using System;
using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
public class HealthAbility : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _startHealth;
    
    public event Action OnHealthChanged;
    public event Action OnDead;
    
    public float Health { get; private set; }

    private void Start()
    {
        Health = _startHealth;
    }

    public void TakeDamage(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("[Health Ability] Damage can`t be lower than 0!");
        }
        
        Health = Health - value <= 0 ? 0 : Health - value;
        
        if (Health == 0)
        {
            OnDead?.Invoke();
        }
        
        OnHealthChanged?.Invoke();
    }

    public void Heal(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("[Health Ability] Heal value can`t be lower than 0!");
        }

        Health = Health + value > _maxHealth ? _maxHealth : Health + value;
        
        OnHealthChanged?.Invoke();
    }

    public void SetHealth(float startHealth, float maxHealth)
    {
        _maxHealth = maxHealth;
        _startHealth = startHealth > maxHealth ? maxHealth : startHealth;
    }
}