using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] bool isRocket = false;
    [SerializeField] float rocketTurnSpeed = 10f;
    [SerializeField] float rocketTargetingDelay = 0.5f;

    [SerializeField] ParticleSystem hitFX;
    [SerializeField] float speed = 50f;
    [SerializeField] float damage = 100f;
    [SerializeField] Vector3 mainDirection;

    [SerializeField] float rocketLifetime = 4f;
    float startTime;

    Transform transform;
    Transform targetEnemy;
    
    Rigidbody rigidbody;
    

    void Awake() {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;

        //Invoke("FindClosestTarget", rocketTargetingDelay);
        
    }



    private void OnTriggerEnter(Collider other) {
        var particleInstance = Instantiate(hitFX, transform.position, transform.rotation);
        if(other.GetComponent<Enemy>() != null) {
            print("enemy detected");
            other.GetComponent<Enemy>().recieveDamage(damage);
        }
        Destroy(particleInstance, 2f);
        Destroy(this.gameObject);
    }

    private void FixedUpdate() {
        FireProjectile();

        if(isRocket && targetEnemy != null && (startTime + rocketLifetime) > Time.time ) {
            RotateTowardsEnemyTarget();
        }
        FindClosestTarget();

        //print((startTime + rocketLifetime) > Time.time);

    }

    private void FireProjectile()
    {
        rigidbody.velocity = transform.forward * speed; ;
    }

    private void RotateTowardsEnemyTarget()
    {
        //Vector3 newDirection = Vector3.RotateTowards(transform.position, targetEnemy.position, rocketTurnSpeed * Time.fixedDeltaTime, 10f);
        //newDirection.Normalize();
        //transform.rotation = Quaternion.LookRotation(newDirection);

        Vector3 direction = (targetEnemy.position - rigidbody.transform.position);
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.forward).y;

        rigidbody.angularVelocity = new Vector3(0f, -rotateAmount * rocketTurnSpeed, 0f);
        // rotateAmount * rocketTurnSpeed * Time.fixedDeltaTime

        rigidbody.velocity = transform.forward * speed;
        
    }


    private void FindClosestTarget()
    {
        if (isRocket)
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
    }
}
