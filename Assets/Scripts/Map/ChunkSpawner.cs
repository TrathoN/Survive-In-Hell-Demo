using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunks;
    [SerializeField] private GameObject player;
    [SerializeField] private float checkerRadius;
    [SerializeField] private LayerMask terrainMask;
    public GameObject currentChunk;
    private Vector3 noTerrainPosition;
    private PlayerMovement pm;

    [Header("Optimization")]
    [SerializeField] private List<GameObject> spawnedChunks;
    [SerializeField] private float maxOpDist;
    [SerializeField] private float optimizerCooldownDur;
    private GameObject latestChunk;
    private float opDist;
    private float optimizerCooldown;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }

        if(pm.moveDir.x > 0 && pm.moveDir.y == 0) //RightMovement
        {
            ChunkDirection("Right Up Chunk");
            ChunkDirection("Right Chunk");
            ChunkDirection("Right Down Chunk");
        }
        if (pm.moveDir.x < 0 && pm.moveDir.y == 0) //LeftMovement
        {
            ChunkDirection("Left Up Chunk");
            ChunkDirection("Left Chunk");
            ChunkDirection("Left Down Chunk");
        }
        if (pm.moveDir.x == 0 && pm.moveDir.y > 0) //UpMovement
        {
            ChunkDirection("Left Up Chunk");
            ChunkDirection("Up Chunk");
            ChunkDirection("Right Up Chunk");
        }
        if (pm.moveDir.x == 0 && pm.moveDir.y < 0) //DownMovement
        {
            ChunkDirection("Right Down Chunk");
            ChunkDirection("Down Chunk");
            ChunkDirection("Left Down Chunk");         
        }
        if (pm.moveDir.x > 0 && pm.moveDir.y > 0) //RightUpMovement
        {
            ChunkDirection("Right Chunk");
            ChunkDirection("Right Up Chunk");
            ChunkDirection("Up Chunk");
        }
        if (pm.moveDir.x > 0 && pm.moveDir.y < 0) //RightDownMovement
        {
            ChunkDirection("Right Chunk");
            ChunkDirection("Right Down Chunk");
            ChunkDirection("Down Chunk");
        }
        if (pm.moveDir.x < 0 && pm.moveDir.y > 0) //LeftUpMovement
        {
            ChunkDirection("Left Chunk");
            ChunkDirection("Left Up Chunk");
            ChunkDirection("Up Chunk");
        }
        if (pm.moveDir.x < 0 && pm.moveDir.y < 0) //LeftDownMovement
        {
            ChunkDirection("Left Chunk");
            ChunkDirection("Left Down Chunk");
            ChunkDirection("Down Chunk");
        }
    }

    void SpawnChunk()
    {
        int random = Random.Range(0, chunks.Count);
        latestChunk = Instantiate(chunks[random], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkDirection(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(direction).position;
            SpawnChunk();
        }
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks) 
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
