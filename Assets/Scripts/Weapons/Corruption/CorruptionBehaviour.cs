using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> damagedEnemies;
    protected override void Start()
    {
        base.Start();
        damagedEnemies = new List<GameObject>();
        transform.localScale = Vector3.one * currentWeaponRadius;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !damagedEnemies.Contains(collision.gameObject))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentWeaponDamage);

            damagedEnemies.Add(collision.gameObject);
        }
        else if (collision.CompareTag("Prop") && !damagedEnemies.Contains(collision.gameObject))
        {
            if (collision.TryGetComponent(out BreakableProps breakble))
            {
                breakble.TakeDamage(currentWeaponDamage);

                damagedEnemies.Add(collision.gameObject);
            }
        }
    }
}
