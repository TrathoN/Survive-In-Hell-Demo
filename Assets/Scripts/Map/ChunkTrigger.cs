using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    [SerializeField] private GameObject targerChunk;
    private ChunkSpawner chunkSpawner;

    void Start()
    {
        chunkSpawner = FindObjectOfType<ChunkSpawner>();    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chunkSpawner.currentChunk = targerChunk;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(chunkSpawner.currentChunk == targerChunk)
            {
                chunkSpawner.currentChunk = null;
            }
        }
    }
}
