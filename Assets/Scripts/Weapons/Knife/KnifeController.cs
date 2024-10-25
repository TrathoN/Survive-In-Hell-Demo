using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class KnifeController : WeaponController
{
    [SerializeField] private List<GameObject> aroundEnemies;
    [SerializeField] private GameObject closestEnemy;
    private int tempIndex;

    protected override void Start()
    {
        base.Start();
        aroundEnemies = new List<GameObject>();
    }

    protected override void Update()
    {        
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        
        StartCoroutine(GroupAttack());
    }

    IEnumerator GroupAttack()
    {
        for (int i = weaponData._weaponProjectile; i > 0; i--)
        {
            CalculateClosestEnemy();
            GameObject weapon = Instantiate(weaponData._weaponPrefab);
            weapon.transform.position = transform.position;

            if (closestEnemy != null)
            {
                weapon.GetComponent<KnifeBehaviour>().DirectionChecker((closestEnemy.transform.position - transform.position).normalized);
            }
            else
            {
                weapon.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedDir);
            }
            yield return new WaitForSeconds(currentWeaponProjectileInterval);
        }
        isAttacking = false;
    }

    private void CalculateClosestEnemy()
    {
        if (aroundEnemies.Count > 0)
        {
            float tempDistance = 99999;
            for (int i = 0; i < aroundEnemies.Count; i++)
            {
                float distance = transform.position.magnitude - aroundEnemies[i].transform.position.magnitude;
                if (distance < 0)
                {
                    distance *= -1;
                }
                if (tempDistance > distance)
                {
                    tempDistance = distance;
                    tempIndex = i;
                }
            }
            closestEnemy = aroundEnemies[tempIndex];
        }
        else
        {
            closestEnemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !aroundEnemies.Contains(collision.gameObject))
        {
            aroundEnemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && aroundEnemies.Contains(collision.gameObject))
        {
            aroundEnemies.Remove(collision.gameObject);
        }
    }
}
