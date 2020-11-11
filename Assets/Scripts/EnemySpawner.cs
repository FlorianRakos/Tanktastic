using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float xMin = 0f;
    [SerializeField] private float xMax = 10f;
    [SerializeField] private float zMin = 0f;
    [SerializeField] private float zMax = 10f;
    [SerializeField] private float y = 1f;
    [SerializeField] private float spawnDelay = 2f;

    [SerializeField] private int desiredEnemys = 2;

    [SerializeField] private GameObject enemy;


    private void Awake() {
        int i = 0;

        while (i < desiredEnemys) {
            i ++;
            StartCoroutine(SpawnEnemy(true));
        }
    }


    public IEnumerator SpawnEnemy(bool spawnsInstantly)
    {
        if (!spawnsInstantly) {
            yield return new WaitForSeconds(spawnDelay);
        }

        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);

        Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);        
    }
}
