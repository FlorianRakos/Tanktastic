using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float zMin;
    [SerializeField] float zMax;
    [SerializeField] float y;

    [SerializeField] GameObject enemy;

    [SerializeField] int desiredEnemys = 200;
    [SerializeField] float spawnDelay = 2f;
    
    int currentEnemys;
    
    bool spawnInProgress = false;

    void Update()
    {
        currentEnemys = FindObjectsOfType<Enemy>().Length;

        if (currentEnemys < desiredEnemys && !spawnInProgress) {
            StartCoroutine(SpawnEnemy());
        }
    }


    IEnumerator SpawnEnemy()
    {
        spawnInProgress = true;
        yield return new WaitForSeconds(spawnDelay);
        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);
        
        Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);
        spawnInProgress = false;

    }
}
