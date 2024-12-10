using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int MaxSpawn = 100;
    public int SpawnCount = 1;
    public float SpawnDelay = 5f;

    private int spawnedCount = 0;
    private float nextSpawnTime = 0;

    void Start()
    {
        nextSpawnTime = Time.time;
    }

    void Update()
    {
        if (spawnedCount > MaxSpawn) return;

        if (nextSpawnTime >= Time.time) return;
 
        for (int i = 0; i < SpawnCount; i++)
        {
            SkeletonManager.Instance.Spawn(transform);
            spawnedCount++;
            if (spawnedCount > MaxSpawn) break;
        }
        nextSpawnTime = Time.time + SpawnDelay;

    }
}
