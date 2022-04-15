using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action NewWave;

    int round = 1;
    int zombiesInRound = 5;
    public static int zombiesLeftInRound = 5;
    int zombiesSpawnedInRound = 0;
    float zombieSpawnTimer = 0;
    public Transform[] zombieSpawnPoints;
    [SerializeField] Zombie zombiePrefab;
    [SerializeField] Zombie zombiePrefab2;
    Zombie chosenZombie;
    int x;

    float countdown = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (zombiesSpawnedInRound < zombiesInRound && countdown == 0)
        {
            if (zombieSpawnTimer > 0.5)
            {
                SpawnZombie();
                zombieSpawnTimer = 0;
            }
            else
            {
                zombieSpawnTimer += Time.deltaTime;
            }
        }
        else if (zombiesLeftInRound == 0)
        {
            StartNextRound();
            NewWave?.Invoke();
            Debug.Log("Wave " + round);
        }

        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = 0;
        }

    }

    void SpawnZombie()
    {
        selectZombieType();
        Vector3 randomSpawnPoint = zombieSpawnPoints[UnityEngine.Random.Range(0, zombieSpawnPoints.Length)].position;
        Instantiate(chosenZombie, randomSpawnPoint, transform.rotation);
        zombiesSpawnedInRound++;
    }

    void StartNextRound()
    {
        zombiesInRound = zombiesLeftInRound = (round + 1) * 5;
        zombiesSpawnedInRound = 0;
        countdown = 10;
        round++;

    }
    void selectZombieType()
    {
        x = UnityEngine.Random.Range(1, 100);
        if (x <= 90)
        {
            chosenZombie = zombiePrefab;
        }
        else
        {
            chosenZombie = zombiePrefab2;
        }
    }
}
