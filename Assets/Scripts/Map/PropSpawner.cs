using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> propSpawnLocations;
    [SerializeField] private List<GameObject> propSpawnObjects;

    void Start()
    {
        SpawnProp();
    }

    private void SpawnProp()
    {
        foreach (GameObject prop in propSpawnLocations)
        {
            int randomRange = Random.Range(0, propSpawnObjects.Count);
            GameObject obj = Instantiate(propSpawnObjects[randomRange], prop.transform.position, Quaternion.identity);
            obj.transform.parent = prop.transform;
        }
    }
}
