using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{


    [SerializeField] private ParticleSystem hitFX;

    [SerializeField] private float speed = 50f;
    [SerializeField] private float damage = 100f;

    [SerializeField] private float projectileLifetime = 4f;

    private float startTime;

    protected Rigidbody rigidbody;
    

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;
        StartCoroutine(ProjectileDecay());
    }


    protected void FixedUpdate() {
        MoveProjectile();
        print("called in Projectile");
    }


    private void MoveProjectile()
    {
        rigidbody.velocity = transform.forward * speed; ;
    }


    private void OnTriggerEnter(Collider other) {                      
        if(other.GetComponent<Enemy>() != null) {
            other.GetComponent<Enemy>().recieveDamage(damage);
        }

        DestroyProjectile();
    }


    private IEnumerator ProjectileDecay()
    {
        yield return new WaitForSeconds(projectileLifetime);
        DestroyProjectile();
    }


    private void DestroyProjectile() {
        Instantiate(hitFX, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}