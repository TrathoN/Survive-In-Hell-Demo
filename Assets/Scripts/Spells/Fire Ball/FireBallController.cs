using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : SpellController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spell = Instantiate(spellData._spellPrefab);
        spell.transform.position = transform.position;
        spell.GetComponent<FireBallBehaviour>().DirectionChecker((enemyPlayer.transform.position - transform.position).normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyPlayer"))
        {
            enemyPlayer = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyPlayer"))
        {
            enemyPlayer = null;
        }
    }

}
