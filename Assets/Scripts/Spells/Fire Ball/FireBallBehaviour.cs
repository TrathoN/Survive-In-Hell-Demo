using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireBallBehaviour : ProjectileSpellBehaviour
{
    private Animator animator;
    private bool isImpacted;
    private bool isExploded;
    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        transform.rotation = projectileRotation;
    }

    void Update()
    {
        projectileLifeSpan -= Time.deltaTime;
        if(projectileLifeSpan <= .0f)
        {
            isImpacted = true;
        }

        if (!isImpacted)
        {
            transform.position += projectileDirection * currentSpellSpeed * Time.deltaTime;
        }
        else
        { 
            if(!isExploded)
            {
                animator.SetTrigger("Impact");
                transform.localScale -= new Vector3(1, 1, 1);
                transform.localScale += new Vector3(3, 3, 3);
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
                isExploded = true;
            }  
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("EnemyPlayer"))
        {
            isImpacted = true;
            
            //EnemyPlayerStats
            //Take Exp Damage
        }
    }
}
