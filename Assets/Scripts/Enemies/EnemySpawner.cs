using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroup;
        public int waveQuota;
        public float spawnInterval;
        public float spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public string enemyName;
        public int enemyCount;
        public float spawnCount;
    }

    public List<Wave> waves;
    public int currentWaveCount;
    public float waveInterval;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached;
    private Transform player;
    private float spawnTimer;

    public List<Transform> spawnPositions;
    

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }
    private void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(NextWave());
        }

        spawnTimer += Time.deltaTime;

        if(spawnTimer > waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    private void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (EnemyGroup enemyGroup in waves[currentWaveCount].enemyGroup)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    private void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach(EnemyGroup enemyGroup in waves[currentWaveCount].enemyGroup)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + spawnPositions[Random.Range(0, spawnPositions.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

}
