using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get { return instance; } }
    private static ResourceManager instance;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private int numberOfBarrelsSpawned = 0;
    [SerializeField] PlayerController player;
    [SerializeField] private float minSpawnTime = 1;
    [SerializeField] private float maxSpawnTime = 5;
    int numberToSpawn = 0;
    float timer = 0;


    private void Start()
    {
        instance = this;
        CheckToSpawn();
    }
    private void Update()
    {
        if(numberToSpawn > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SpawnBarrel();
            }
        }
    }

    public void CheckToSpawn()
    {
        if(Captain.myCaptain.hullHoles - (player.plank + numberOfBarrelsSpawned) > 3)
        {
            numberToSpawn++;
            if (numberToSpawn == 1)
            {
                timer = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }

    public void BarrelPickedup()
    {
        numberOfBarrelsSpawned--;
        CheckToSpawn();
    }

    public void SpawnBarrel()
    {
        numberToSpawn--;
        if(numberToSpawn > 0)
        {
            timer = Random.Range(minSpawnTime, maxSpawnTime);
        }
        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(barrelPrefab, spawnPoints[i].position, Quaternion.identity, transform);
    }
}
