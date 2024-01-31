using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

public class AttackAbility : MonoBehaviour
{
    [Inject] private BoostersSettings _boostersSettings;
    [Inject] private WeaponSettings _weaponSettings;

    private bool _canAttack;
    private int _currentAmmoAmount;

    private void Start()
    {
        _currentAmmoAmount = _weaponSettings.AmmoAmount;
    }
    
    public void Attack(bool hasDamageBooster, bool hasWeaponBooster)
    {
        if (_weaponSettings.IsShootableWeapon)
        {
            ShootAttack(hasDamageBooster, hasWeaponBooster);
        }
        else
        {
            MeleeAttack(hasDamageBooster, hasWeaponBooster);
        }
    }

    private void ShootAttack(bool hasDamageBooster, bool hasWeaponBooster)
    {
        if (_canAttack == true && _currentAmmoAmount > 0)
        {
            _canAttack = false;
            
            ShootAttack(hasDamageBooster);

            if (_currentAmmoAmount > 0)
            {
                StartAttackDelay(hasWeaponBooster);
            }
            else
            {
                StartReloadDelay(hasWeaponBooster);
            }
        }
    }

    private void MeleeAttack(bool hasDamageBooster, bool hasWeaponBooster)
    {
        if (_canAttack == true)
        {
            _canAttack = false;
            
            MeleeAttack(hasDamageBooster);

            StartAttackDelay(hasWeaponBooster);
        }
    }

    private void ShootAttack(bool hasDamageBooster)
    {
        Observable.Start(() =>
        {
            var ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _weaponSettings.AttackDistance))
            {
                
            }
        });
    }

    private void MeleeAttack(bool hasDamageBooster)
    {
        Observable.Start(() =>
        {
            var colliders = Physics.OverlapSphere(transform.position, _weaponSettings.AttackDistance / 2);

            foreach (var collider in colliders)
            {
                
            }
        });
    }
    
    private void StartAttackDelay(bool hasWeaponBooster)
    {
        Observable.Start(() =>
        {
            var delay = _weaponSettings.AttackDelay;

            if (hasWeaponBooster == true)
            {
                delay -= _boostersSettings.ReducingAttackDelay;
            }

            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ => { _canAttack = true; });
        });
    }
    
    private void StartReloadDelay(bool hasWeaponBooster)
    {
        Observable.Start(() =>
        {
            var delay = _weaponSettings.ReloadDelay;

            if (hasWeaponBooster == true)
            {
                delay -= _boostersSettings.ReducingReloadDelay;
            }

            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ =>
            {
                _currentAmmoAmount = _weaponSettings.AmmoAmount;
                _canAttack = true;
            });
        });
    }
}