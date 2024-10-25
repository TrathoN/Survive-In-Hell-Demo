using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyFood : PickUp, ICollectable
{
    [SerializeField] private int restoreHealth;
    public void Pull()
    {
        //PlayerStats player = FindAnyObjectByType<PlayerStats>();
        //player.RestoreHealth(restoreHealth);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
        {
            PlayerStats player = FindAnyObjectByType<PlayerStats>();
            player.RestoreHealth(restoreHealth);
        }

    }
}
