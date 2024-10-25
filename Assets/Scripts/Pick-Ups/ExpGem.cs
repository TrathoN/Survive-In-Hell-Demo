using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : PickUp, ICollectable
{
    [SerializeField] private int exp;
    [SerializeField] private float pullSpeed;
    private bool isPulling;

    private void Update()
    {
        if(isPulling)
        {
            Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 forceDir = (playerPosition.position - transform.position).normalized;
            transform.position += forceDir * pullSpeed * Time.deltaTime;
        }
    }
    public void Pull()
    {
        isPulling = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("Player"))
        {
            PlayerStats player = FindAnyObjectByType<PlayerStats>();
            player.AddXp(exp);
        }

    }
}
