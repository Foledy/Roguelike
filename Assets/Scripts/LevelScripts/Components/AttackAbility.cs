using UniRx;
using UnityEditor.PackageManager;
using UnityEngine;

public class AttackAbility : MonoBehaviour
{
    [SerializeField] private WeaponSettings _weaponSettings;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private LayerMask _playerLayer;

    public float AttackDistance => _weaponSettings.AttackDistance;
    
    private int _currentAmmoAmount;
    private bool _canAttack;
    private Camera _camera;

    private void OnEnable()
    {
        _camera = Camera.main;
        _currentAmmoAmount = _weaponSettings.AmmoAmount;
        _canAttack = true;
    }

    public void Attack(float damageMultiplier, float delayDivider)
    {
        if (_weaponSettings.IsShootingWeapon)
        {
            ShootAttack(damageMultiplier, delayDivider);
        }
        else
        {
            MeleeAttack(damageMultiplier, delayDivider);
        }
    }

    private void ShootAttack(float damageMultiplier, float delayDivider)
    {
        if (_canAttack != true || _currentAmmoAmount <= 0) return;
        
        _canAttack = false;
        _currentAmmoAmount -= 1;
            
        PerformShootAttack(damageMultiplier);
        PlayAttackClip(_weaponSettings.AttackClip);
        PlayVFX();

        if (_currentAmmoAmount > 0)
        {
            StartAttackDelay(damageMultiplier);
        }
        else
        {
            StartReloadDelay(delayDivider);
        }
    }

    private void MeleeAttack(float damageMultiplier, float delayDivider)
    {
        if (_canAttack == true)
        {
            _canAttack = false;
            
            PerformMeleeAttack(damageMultiplier);

            StartAttackDelay(delayDivider);
        }
    }

    private void PerformShootAttack(float damageMultiplier)
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hitInfo, _weaponSettings.AttackDistance, ~_playerLayer) == true)
        {
            if (hitInfo.collider.TryGetComponent(out HealthHandler health) == true)
            {
                var damage = _weaponSettings.Damage * damageMultiplier;
                
                health.AddActionToQueue(HealthActionType.TakeDamage, damage);
            }
        }
    }

    private void PerformMeleeAttack(float damageMultiplier)
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
                    
                    var damage = _weaponSettings.Damage * damageMultiplier;
                    
                    health.AddActionToQueue(HealthActionType.TakeDamage, damage);
                }
            }
        });
    }
    
    private void StartAttackDelay(float delayDivider)
    {
        var delay = _weaponSettings.AttackDelay / delayDivider;

        Observable.Timer(System.TimeSpan.FromSeconds(delay)).SubscribeOnMainThread().
            Subscribe(_ => { _canAttack = true; }).AddTo(this);
    }

    private void StartReloadDelay(float delayDivider)
    {
        var delay = _weaponSettings.ReloadDelay / delayDivider;

        Observable.Timer(System.TimeSpan.FromSeconds(delay)).Subscribe(_ =>
        {
            _currentAmmoAmount = _weaponSettings.AmmoAmount;
            _canAttack = true;
        });
    }

    private void PlayAttackClip(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    private void PlayVFX()
    {
        var flash = Instantiate(_weaponSettings.MuzzlePrefab, _muzzleTransform);
    }
}