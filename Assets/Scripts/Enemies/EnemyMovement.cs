using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private Transform playerTransform;
    private EnemyStats enemy;
    private float lastPosition;
    private float scaleEnemy;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        scaleEnemy = transform.localScale.x;
        playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        lastPosition = transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemy.currentMoveSpeed * Time.deltaTime);

        EnemyDir();
    }

    private void EnemyDir()
    {
        if ((transform.position.x - lastPosition) < 0)
        {
            transform.localScale = new Vector3(-scaleEnemy, scaleEnemy, scaleEnemy);
        }
        else
        {
            transform.localScale = new Vector3(scaleEnemy, scaleEnemy, scaleEnemy);
        }
    }
}
