using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float dissolveDuration = 1f;

    private MeshRenderer meshRenderer;
    private EnemySpawner enemySpawner;

    private float spawndelay;
    private float dissolveValue = 1f;
    

    private bool validSpawn = true;
    private bool isAlive = true;


    void Awake() {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(CheckSpawnValidity());
    }


    private void Update() {
        DissolvingEffect();
    }

    private void DissolvingEffect () {
        if (isAlive && dissolveValue > 0f) {
            dissolveValue -= Time.deltaTime * (1/dissolveDuration);
        } 
        if (!isAlive && dissolveValue < 1f) {
            dissolveValue += Time.deltaTime * (1/dissolveDuration);
        }

        meshRenderer.material.SetFloat("Vector1_C333254E", dissolveValue);
    }


    private void OnCollisionEnter(Collision other) {
       validSpawn = false;
    }
  

    IEnumerator CheckSpawnValidity () {
        yield return new WaitForFixedUpdate();

        if (validSpawn) {
            meshRenderer.enabled = true;
        }
        else {
            enemySpawner.StartCoroutine(enemySpawner.SpawnEnemy(true));
            Destroy(this.gameObject);
        }
    }


    public void recieveDamage (float damage) {
        health -= damage;

        if(health <= 0f) {
            enemySpawner.StartCoroutine(enemySpawner.SpawnEnemy(false));
            isAlive = false;
            GetComponent<Collider>().enabled = false;
            
            Destroy(this.gameObject, dissolveDuration);
        }
    }    
}
