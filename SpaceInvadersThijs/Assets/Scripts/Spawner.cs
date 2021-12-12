using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // public variables
    public GameObject alienNormal;
    public GameObject alienMedium;
    public GameObject alienHard;
    public GameObject alienBoss;    
    public int maxWave;
    public int currentWave;

    // private variables
    private Vector3 spawnPoint;
    private float x;
    private float minDistance;
    private int amount;
    private int alreadySpawned;

    // Start the first wave
    void Start()
    {
        GameStats.gameStatsRef.spawnerRef = this;
        currentWave = 1;
        minDistance = 1;
        StartWave();
    }

    // start spawn the aliens on random locations
    public void StartWave()
    {
        amount = currentWave * 2;
        alreadySpawned = 0;
        GameStats.gameStatsRef.countEnemies = amount;
        GameStats.gameStatsRef.countWaves = currentWave;
        if (currentWave == 5)
        {
            SpawnPosition();
            Instantiate(alienBoss, spawnPoint, Quaternion.identity);
        }
        else
        {
            if (amount <= 5)
            {
                InstantiateAlien(amount);
            }
            else
            {
                StartCoroutine(InstantiateInWaves());
            }
        }        
    }

    // if many enemies are to be spawned, then they
    // must be spawned one after the other so as not to overlap
    IEnumerator InstantiateInWaves()
    {
        int spawn;
        if (amount - (alreadySpawned + 5) > 0)
        {
            spawn = 5;
            alreadySpawned += spawn;
            
        }
        else if (amount - (alreadySpawned + 4) > 0)
        {
            spawn = 4;
            alreadySpawned += spawn;
        }
        else if (amount - (alreadySpawned + 3) > 0)
        {
            spawn = 3;
            alreadySpawned += spawn;
        }
        else if (amount - (alreadySpawned + 2) > 0)
        {
            spawn = 2;
            alreadySpawned += spawn;
        }
        else
        {
            spawn = 1;
            alreadySpawned += spawn;
        }
        InstantiateAlien(spawn);
        yield return new WaitForSeconds(3);
        if (alreadySpawned < amount)
        {
            StartCoroutine(InstantiateInWaves());
        }
    }

    // the actual spawning is carried out here
    private void InstantiateAlien(int pAmount)
    {
        for (int i = 0; i < pAmount; i++)
        {
            SpawnPosition();
            int j = Random.Range(0, 25);
            if (j < 20 && j % 2 == 0)
            {
                Instantiate(alienNormal, spawnPoint, Quaternion.identity);
            }
            else if (j < 20 && j % 2 != 0)
            {
                Instantiate(alienMedium, spawnPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(alienHard, spawnPoint, Quaternion.identity);
            }
        }
    }

    // the spawn position is calculated here
    private void SpawnPosition()
    {
        spawnPoint = transform.position;
        x = Random.Range(-8f, 8f);
        spawnPoint.x += x;
        spawnPoint.z = 0;
        Collider2D[] neighbours = Physics2D.OverlapCircleAll(spawnPoint, minDistance);
        if (neighbours.Length > 0)
        {
            SpawnPosition();
        }
    }
}