using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject EnemyPrefab;
    public GameObject EnemyPrefab2;
    public FollowObject cameraMover;

    public int spawnRateMax = 5;
    public int spawnRateMin = 1;

    public Transform[] spawnPositions;
    public List<Transform> enemiesSpawned = new List<Transform>();
    
    public bool spawnEnemiesNow;

    bool holdPlayer;
    bool dead;

    void Update()
    {
        if (spawnEnemiesNow)
        {
            //holdPlayer = true;
            SpawnEnemies();
            SpawnEnemies2();
            spawnEnemiesNow = false;
        }

        if (holdPlayer)
        {
            cameraMover.enabled = false;
        }
        else
        {
            cameraMover.enabled = true;
        }

        if (enemiesSpawned.Count == 0)
        {
            holdPlayer = false;
        }
        
        if (enemiesSpawned.Count != 0 )
        {
            cameraMover.enabled = false;
        }
        else
        {
            cameraMover.enabled = true;
        }
        
        if (dead)
        {
            
        }       
    }

    void SpawnEnemies()
    {
        int ranValue = Random.Range(spawnRateMin, spawnRateMax + 1);

        for (int i =0; i < ranValue; i++)
        {
            int ranSpawn = Random.Range(0, spawnPositions.Length);
            GameObject go = Instantiate(EnemyPrefab, spawnPositions[ranSpawn].position, Quaternion.identity) as GameObject;
            enemiesSpawned.Add(go.transform);
        }


    }

    void SpawnEnemies2()
    {
        int ranValue = Random.Range(spawnRateMin, spawnRateMax + 1);

        for (int i = 0; i < ranValue; i++)
        {
            int ranSpawn = Random.Range(0, spawnPositions.Length);
            GameObject go = Instantiate(EnemyPrefab2, spawnPositions[ranSpawn].position, Quaternion.identity) as GameObject;
            enemiesSpawned.Add(go.transform);
        }


    }
}            



