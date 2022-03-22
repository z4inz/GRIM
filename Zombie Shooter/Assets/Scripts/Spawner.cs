using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float nextSpawnTime;
    [SerializeField] float spawnDelay = 5f;
    [SerializeField] Zombie zombiePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn())
        {
            Spawn();
        }
    }
    void Spawn()
    {
        nextSpawnTime = Time.time + spawnDelay;
        var zombie = Instantiate(zombiePrefab, transform.position, transform.rotation);

    }

    bool readyToSpawn() => Time.time >= nextSpawnTime;
}