using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
        transform.rotation = projectileRotation;
    }

    void Update()
    {
        transform.position += projectileDirection * currentWeaponSpeed * Time.deltaTime;
    }
}
