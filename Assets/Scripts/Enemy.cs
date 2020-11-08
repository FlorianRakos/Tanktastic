using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;

    MeshRenderer meshRenderer;

    bool validSpawn = true;

    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(CheckSpawnValidity());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Projectile>() == null && other.GetComponent<PlayerMovement>() == null) {
            validSpawn = false;
            print("trigger");

        }
    }

    private void OnCollisionEnter(Collision other) {
       validSpawn = false;
    }

    public void recieveDamage (float damage) {
        health -= damage;
        if(health <= 0f) {
            Destroy(this.gameObject);

        }
    }

    IEnumerator CheckSpawnValidity () {
        yield return new WaitForFixedUpdate();
        if (validSpawn) {
            meshRenderer.enabled = true;
        }
        else {
            Destroy(this.gameObject);
            print("non valid spawn");
        }
    }

}
