using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] endPoints;
    [SerializeField] private float enemySpawnTime = 6f;
    public int BaseEnemiesSpawnRate = 5;
    private int numOfEnemiesToSpawn;
    private int currentNumOfEnemies = 0;
    private int numberOfEnemiesAlive = 0;

    [Header("Waves")]
    [SerializeField] private float betweenWaveTime = 15f;
    private int waveNumber = 0;
    private bool waveFinished = false; // all enemies killed
    private bool waveOngoing = true; // spawning new enemies
    [SerializeField] private float startDelayTime = 5;

    private float timer;
    private SoundM sound;
    
    void Start()
    {
        waveFinished = false; 
        waveOngoing = true;
        numOfEnemiesToSpawn = BaseEnemiesSpawnRate;
        timer = startDelayTime;
        sound = GetComponent<SoundM>();
    }

    void Update()
    {
        if(timer <= 0)
        {
            if (waveOngoing)
            {
                SpawnEnemy();
            }
            else if (waveFinished)
            {
                ResetWave();
            }
        }
        else if(waveOngoing || (!waveOngoing && waveFinished))
        {
            timer -= Time.deltaTime;
        }
    }

    public void SpawnEnemy()
    {
        int i = Random.Range(0, 2);
        EnemyController enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity, transform);
        enemy.Init(spawnPoints[i], endPoints[i], this);
        numberOfEnemiesAlive++;
        currentNumOfEnemies++;
        if (currentNumOfEnemies < numOfEnemiesToSpawn)
        {
            timer = enemySpawnTime + Random.Range(-0.5f, 1f);
        }
        else
        {
            waveOngoing = false;
        }
    }

    public void EnemyKilled()
    {
        numberOfEnemiesAlive--;
        if(!waveOngoing && numberOfEnemiesAlive <= 0)
        {
            waveFinished = true;
            timer = betweenWaveTime;
            waveNumber++;
            numOfEnemiesToSpawn = waveNumber + BaseEnemiesSpawnRate;
        }
        sound.Play("Death");
    }

    public void ResetWave()
    {
        waveFinished = false;
        waveOngoing = true;
        currentNumOfEnemies = 0;
        numberOfEnemiesAlive = 0;
        SpawnEnemy();
    }
}
