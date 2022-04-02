using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] endPoints;
    [SerializeField] private float enemySpawnTime = 6f;
    public int numOfEnemiesToSpawn = 5;
    private int currentNumOfEnemies = 0;
    private float timer;
    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        if(timer <= 0 && currentNumOfEnemies < numOfEnemiesToSpawn)
        {
            SpawnEnemy();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void SpawnEnemy()
    {
        int i = Random.Range(0, 2);
        EnemyController enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity, transform);
        enemy.Init(spawnPoints[i], endPoints[i]);
        timer = enemySpawnTime;
        currentNumOfEnemies++;
    }
}
