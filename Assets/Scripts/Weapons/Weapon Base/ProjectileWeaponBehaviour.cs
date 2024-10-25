using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 projectileDirection;
    protected Quaternion projectileRotation;
    [SerializeField] protected WeaponScriptableObject weaponData;
    [SerializeField] private float projectileLifeSpan;

    protected float currentWeaponDamage;
    protected float currentWeaponSpeed;
    protected int currentWeaponPierce;
    protected float currentWeaponCooldownDur;
    protected int currentWeaponProjectile;

    protected virtual void Start()
    {
        Destroy(gameObject,projectileLifeSpan);

        currentWeaponDamage = weaponData._weaponDamage;
        currentWeaponSpeed = weaponData._weaponSpeed;
        currentWeaponPierce = weaponData._weaponPierce;
        currentWeaponCooldownDur = weaponData._weaponCooldownDur;
        currentWeaponProjectile = weaponData._weaponProjectile;
    }

    public virtual void DirectionChecker(Vector3 dir)
    {
        projectileDirection = dir;
        float enemyRotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        projectileRotation = Quaternion.Euler(0.0f, 0.0f, enemyRotationZ);

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentWeaponDamage);
            ReducePierce();
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.TryGetComponent(out BreakableProps breakble))
            {
                breakble.TakeDamage(currentWeaponDamage);
                ReducePierce();
            }
        }
    }

    private void ReducePierce()
    {
        currentWeaponPierce--;
        if(currentWeaponPierce <= 0)
        {
            Destroy(gameObject);
        }
    }

}
