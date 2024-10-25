using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        GameObject weapon = Instantiate(weaponData._weaponPrefab);
        weapon.transform.position = transform.position;
        weapon.transform.parent = transform;

        isAttacking = false;
    }
}
