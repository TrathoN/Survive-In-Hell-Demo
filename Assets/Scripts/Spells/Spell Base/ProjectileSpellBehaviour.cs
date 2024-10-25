using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpellBehaviour : MonoBehaviour
{
    protected Vector3 projectileDirection;
    protected Quaternion projectileRotation;
    [SerializeField] protected SpellScriptableObject spellData;
    [SerializeField] protected float projectileLifeSpan;

    protected float currentSpellDamage;
    protected float currentSpellExplosionDamage;
    protected float currentSpellSpeed;
    protected int currentSpellPierce;
    protected float currentSpellCooldownDur;
    protected float currentSpellExplosionRadius;

    protected virtual void Start()
    {
        //Destroy(gameObject, projectileLifeSpan);

        currentSpellDamage = spellData._spellDamage;
        currentSpellExplosionDamage = spellData._spellExplosionDamage;
        currentSpellSpeed = spellData._spellSpeed;
        currentSpellPierce = spellData._spellPierce;
        currentSpellCooldownDur = spellData._spellCooldownDur;
        currentSpellExplosionRadius = spellData._spellExplosionRadius;
    }

    public virtual void DirectionChecker(Vector3 dir)
    {
        projectileDirection = dir;
        float enemyPlayerRotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        projectileRotation = Quaternion.Euler(0.0f, 0.0f, enemyPlayerRotationZ);

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyPlayer"))
        {
            //EnemyPlayerStats
            //TakeDamage
        }
        else if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentSpellDamage);
            ReducePierce();
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.TryGetComponent(out BreakableProps breakble))
            {
                breakble.TakeDamage(currentSpellDamage);
            }
        }
    }

    private void ReducePierce()
    {
        currentSpellPierce--;
        if (currentSpellPierce <= 0)
        {
            Destroy(gameObject);
        }
    }

}
