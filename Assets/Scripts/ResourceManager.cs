using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnBarrel();
    }

    public void SpawnBarrel()
    {
        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(barrelPrefab, spawnPoints[i].position, Quaternion.identity, transform);
    }
}
