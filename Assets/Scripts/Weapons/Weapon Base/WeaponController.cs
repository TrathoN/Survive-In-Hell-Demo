using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Attributes")]
    public WeaponScriptableObject weaponData;
    private float currentWeaponCooldown;
    protected float currentWeaponProjectileInterval;
    protected PlayerMovement pm;
    protected bool isAttacking;

    protected virtual void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        currentWeaponCooldown = GetCooldown();
        currentWeaponProjectileInterval = GetProjectileInterval();
    }

    protected virtual void Update()
    {
        if(!isAttacking)
        {
            currentWeaponCooldown -= Time.deltaTime;
        }
        if (currentWeaponCooldown <= .0f)
        {
            isAttacking = true;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentWeaponCooldown = GetCooldown();
        currentWeaponProjectileInterval = GetProjectileInterval();
    }

    private float GetCooldown()
    {
        return FindObjectOfType<PlayerStats>().CurrentCooldown * weaponData._weaponCooldownDur;
    }

    private float GetProjectileInterval()
    {
        return FindObjectOfType<PlayerStats>().CurrentCooldown * weaponData._weaponProjectileInterval;
    }
}
