using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArcher : MonoBehaviour
{
    public Transform player;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float runSpeed = 5f;
    public float shootingCooldown = 2f;
    public float shootingRange = 10f;
    public float arrowSpeed = 10f;

    private Animator animator;
    private bool canShoot = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ShootingLoop());
        player= GameObject.Find("Character").GetComponent<Transform>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > shootingRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StopRunning();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, runSpeed * Time.deltaTime);
        animator.SetBool("Running", true);
    }

    private void StopRunning()
    {
        animator.SetBool("Running", false);
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (player.position - arrowSpawnPoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
        arrowRigidbody.velocity = direction * arrowSpeed;

        animator.SetTrigger("Attack");
    }

    private IEnumerator ShootingLoop()
    {
        while (true)
        {
            if (player != null && Vector2.Distance(transform.position, player.position) <= shootingRange && canShoot)
            {
                ShootArrow();
                canShoot = false;
                yield return new WaitForSeconds(shootingCooldown);
                canShoot = true;
            }

            yield return null;
        }
    }
}

