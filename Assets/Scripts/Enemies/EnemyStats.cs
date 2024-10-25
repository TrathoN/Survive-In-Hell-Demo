using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentDamage;

    public float despawnDis = 50f;
    private Transform player;

    private void Awake()
    {
        currentHealth = enemyData._enemyMaxHealth;
        currentMoveSpeed = enemyData._enemyMoveSpeed;
        currentDamage = enemyData._enemyDamage;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, player.position) >= despawnDis)
        {
            ReturnEnemy();
        }
    }
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        EnemySpawner eS = FindObjectOfType<EnemySpawner>();
        eS.OnEnemyKilled();
    }

    private void ReturnEnemy()
    {
        EnemySpawner eS = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + eS.spawnPositions[Random.Range(0, eS.spawnPositions.Count)].position;
    }
}
