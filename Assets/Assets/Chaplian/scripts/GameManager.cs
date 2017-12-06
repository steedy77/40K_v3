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

    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
    }

    void Update()
    {
        if (spawnEnemiesNow)
        {
            //holdPlayer = true;
            SpawnEnemies();
            SpawnEnemies2();
            spawnEnemiesNow = false;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (gameOver)
        {
            restartText.text = "Press R to Restart";
            restart = true;
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

    public void GameOver()
    {
        StartCoroutine("gameIsOver");
    }

    IEnumerator gameIsOver()

    {
        yield return new WaitForSeconds(3.5f);
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}            



