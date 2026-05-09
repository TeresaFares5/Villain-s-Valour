using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldestWand : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1.0f;
    public float projectileSpeed = 10.0f;

    private float nextFireTime;
    private Transform currentTarget; // Track the current target

    private void Start()
    {
        currentTarget = FindClosestEnemy();
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireAtTarget();
            nextFireTime = Time.time + 1.0f / fireRate;
        }
    }

    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    private void FireAtTarget()
    {
        if (currentTarget != null)
        {
            Vector2 direction = currentTarget.position - transform.position;
            direction.Normalize();

            // Create and shoot a projectile
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }
        else
        {
            // No current target, find the next closest enemy
            currentTarget = FindClosestEnemy();
        }
    }
}

