using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponScriptableObject weaponData;
    [SerializeField] private float meleeLifeSpan;

    protected float currentWeaponDamage;
    protected float currentWeaponSpeed;
    protected int currentWeaponPierce;
    protected float currentWeaponCooldownDur;
    protected float currentWeaponRadius;

    private void Awake()
    {
        currentWeaponDamage = weaponData._weaponDamage;
        currentWeaponSpeed = weaponData._weaponSpeed;
        currentWeaponPierce = weaponData._weaponPierce;
        currentWeaponCooldownDur = weaponData._weaponCooldownDur;
        currentWeaponRadius = weaponData._weaponRadius;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, meleeLifeSpan);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentWeaponDamage);
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.TryGetComponent(out BreakableProps breakble))
            {
                breakble.TakeDamage(currentWeaponDamage);
            }
        }

    }
}
