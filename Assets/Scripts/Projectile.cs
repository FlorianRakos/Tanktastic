using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private bool isRocket = false;
    [SerializeField] private float rocketTurnSpeed = 10f;
    [SerializeField] private float rocketTargetingDelay = 0.5f;

    [SerializeField] private ParticleSystem hitFX;
    [SerializeField] private Quaternion hitFXRotation;

    [SerializeField] private float speed = 50f;
    [SerializeField] private float damage = 100f;

    [SerializeField] private float projectileLifetime = 4f;

    private float startTime;

    private Transform targetEnemy;

    private Rigidbody rigidbody;
    

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;
        StartCoroutine(ProjectileDecay());
    }


    void FixedUpdate() {
        MoveProjectile();

        if(isRocket) {
            FindClosestTarget();

            if(targetEnemy != null) {
               RotateTowardsEnemyTarget(); 
            }                        
        }
    }


    private void MoveProjectile()
    {
        rigidbody.velocity = transform.forward * speed; ;
    }


    private void FindClosestTarget()
    {        
        var enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length == 0) 
        {
            targetEnemy = null; 
        }

        else
        {
            Transform closestEnemy = enemies[0].transform;

            foreach (Enemy enemy in enemies)
            {
                var distanceCurrent = Vector3.Distance(enemy.transform.position, transform.position);
                var distanceClosest = Vector3.Distance(closestEnemy.transform.position, transform.position);
                if (distanceCurrent < distanceClosest)
                {
                    closestEnemy = enemy.transform;
                }
            }
            targetEnemy = closestEnemy;
        }
        
    }


    private void RotateTowardsEnemyTarget()
    {
        Vector3 direction = (targetEnemy.position - rigidbody.transform.position);
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.forward).y;

        rigidbody.angularVelocity = new Vector3(0f, -rotateAmount * rocketTurnSpeed, 0f);
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