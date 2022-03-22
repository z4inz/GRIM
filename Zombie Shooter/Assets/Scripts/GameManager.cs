using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int round = 1;
    int zombiesInRound = 5;
    public static int zombiesLeftInRound = 5;
    int zombiesSpawnedInRound = 0;
    float zombieSpawnTimer = 0;
    public Transform[] zombieSpawnPoints;
    [SerializeField] Zombie zombiePrefab;

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
            if (zombieSpawnTimer > 2)
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
            Debug.Log("Round " + round);
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
        Vector3 randomSpawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)].position;
        Instantiate(zombiePrefab, randomSpawnPoint, transform.rotation);
        zombiesSpawnedInRound++;
    }

    void StartNextRound()
    {
        zombiesInRound = zombiesLeftInRound = round * 10;
        zombiesSpawnedInRound = 0;
        countdown = 5;
        round++;
    }
}
