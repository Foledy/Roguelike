using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

public class AttackAbility : MonoBehaviour
{
    [Inject] private WeaponSettings _weaponSettings;

    private bool _canAttack;
    private int _currentAmmoAmount;

    private void Start()
    {
        _currentAmmoAmount = _weaponSettings.AmmoAmount;
    }
    
    public void Attack(DamageBooster damageBooster, WeaponBooster weaponBooster)
    {
        if (_weaponSettings.IsShootableWeapon)
        {
            ShootAttack(damageBooster, weaponBooster);
        }
        else
        {
            MeleeAttack(damageBooster, weaponBooster);
        }
    }

    private void ShootAttack(DamageBooster damageBooster, WeaponBooster weaponBooster)
    {
        if (_canAttack == true && _currentAmmoAmount > 0)
        {
            _canAttack = false;
            
            ShootAttack(damageBooster);

            if (_currentAmmoAmount > 0)
            {
                StartAttackDelay(weaponBooster);
            }
            else
            {
                StartReloadDelay(weaponBooster);
            }
        }
    }

    private void MeleeAttack(DamageBooster damageBooster, WeaponBooster weaponBooster)
    {
        if (_canAttack == true)
        {
            _canAttack = false;
            
            MeleeAttack(damageBooster);

            StartAttackDelay(weaponBooster);
        }
    }

    private void ShootAttack(DamageBooster damageBooster)
    {
        Observable.Start(() =>
        {
            var ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _weaponSettings.AttackDistance))
            {
                
            }
        });
    }

    private void MeleeAttack(DamageBooster damageBooster)
    {
        Observable.Start(() =>
        {
            var colliders = Physics.OverlapSphere(transform.position, _weaponSettings.AttackDistance / 2);

            foreach (var collider in colliders)
            {
                
            }
        });
    }
    
    private void StartAttackDelay(WeaponBooster weaponBooster)
    {
        Observable.Start(() =>
        {
            var delay = _weaponSettings.AttackDelay;

            if (weaponBooster.IsActive == true)
            {
                delay -= weaponBooster.ReducingAttackDelay;
            }

            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ => { _canAttack = true; });
        });
    }
    
    private void StartReloadDelay(WeaponBooster weaponBooster)
    {
        Observable.Start(() =>
        {
            var delay = _weaponSettings.ReloadDelay;

            if (weaponBooster.IsActive == true)
            {
                delay -= weaponBooster.ReducingReloadDelay;
            }

            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
            {
                _currentAmmoAmount = _weaponSettings.AmmoAmount;
                _canAttack = true;
            });
        });
    }
}