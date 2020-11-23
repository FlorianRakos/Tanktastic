using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float rocketTurnSpeed = 10f;
    [SerializeField] private float rocketTargetingDelay = 0.5f;

    private Transform targetEnemy;

    private void FixedUpdate() {
        base.FixedUpdate();
        FindClosestTarget();
        
        if (targetEnemy != null)
        {
            RotateTowardsEnemyTarget();
        }
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
}
