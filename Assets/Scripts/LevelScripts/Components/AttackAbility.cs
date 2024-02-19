using UniRx;
using UnityEngine;

public class AttackAbility : MonoBehaviour
{
    [SerializeField] private WeaponSettings _weaponSettings;

    private int _currentAmmoAmount;
    private bool _canAttack;

    private void OnEnable()
    {
        _currentAmmoAmount = _weaponSettings.AmmoAmount;
        _canAttack = true;
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

            _currentAmmoAmount -= 1;

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
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _weaponSettings.AttackDistance) == true)
        {
            Debug.Log("Attacking.....");
            if (hitInfo.collider.TryGetComponent(out HealthHandler health) == true)
            {
                Debug.Log("Attacking.....");
                if (hitInfo.collider.gameObject == gameObject)
                {
                    return;
                }

                var damage = _weaponSettings.Damage;

                if (damageBooster.IsActive == true)
                {
                    damage *= damageBooster.Multiplier;
                }
                
                health.AddActionToQueue(HealthActionType.TakeDamage, damage);
            }
        }
    }

    private void MeleeAttack(DamageBooster damageBooster)
    {
        Observable.Start(() =>
        {
            var colliders = Physics.OverlapSphere(transform.position, _weaponSettings.AttackDistance / 2);

            foreach (var target in colliders)
            {
                if (target.TryGetComponent(out HealthHandler health) == true)
                {
                    if (target.gameObject == gameObject)
                    {
                        continue;
                    }
                    
                    var damage = _weaponSettings.Damage;

                    if (damageBooster.IsActive == true)
                    {
                        damage *= damageBooster.Multiplier;
                    }
                    
                    health.AddActionToQueue(HealthActionType.TakeDamage, damage);
                }
            }
        });
    }
    
    private void StartAttackDelay(WeaponBooster weaponBooster)
    {
        var delay = _weaponSettings.AttackDelay;

        if (weaponBooster.IsActive == true)
        {
            delay /= weaponBooster.ReducingAttackDelay;
        }

        Observable.Timer(System.TimeSpan.FromSeconds(delay)).SubscribeOnMainThread().
            Subscribe(_ => { _canAttack = true; }).AddTo(this);
    }
    
    private void StartReloadDelay(WeaponBooster weaponBooster)
    {
        var delay = _weaponSettings.ReloadDelay;

        if (weaponBooster.IsActive == true)
        {
            delay /= weaponBooster.ReducingReloadDelay;
        }

        Observable.Timer(System.TimeSpan.FromSeconds(delay)).Subscribe(_ =>
        {
            _currentAmmoAmount = _weaponSettings.AmmoAmount;
            _canAttack = true;
        });
    }
}