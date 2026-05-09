using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWizardAI : MonoBehaviour
{
    public Transform player;
    public float movementSpeed = 3f;
    public float stoppingRadius = 2f;
    public GameObject lightningStrikePrefab;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isAttacking = false;

    private static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private static readonly int StopsAttackingHash = Animator.StringToHash("StopsAttacking");

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check the direction of the player
        if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Play attack animation once when reaching the stopping distance
        if (distanceToPlayer <= stoppingRadius && !isAttacking)
        {
            animator.SetTrigger(IsAttackingHash);
            isAttacking = true;
        }
        else if (distanceToPlayer > stoppingRadius && isAttacking)
        {
            animator.SetTrigger(StopsAttackingHash);
            isAttacking = false;
        }

        // Move towards the player only if the distance is greater than the stopping radius
        if (distanceToPlayer > stoppingRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Called by an animation event to reset the IsAttacking trigger
    private void ResetIsAttacking()
    {
        animator.ResetTrigger(IsAttackingHash);
    }

    // Called by an animation event to perform the lightning attack
    private void PerformLightningAttack()
    {
        Debug.Log("Performing lightning attack!");

        // Determine if the lightning should visually flip horizontally randomly
        bool shouldFlipHorizontally = Random.Range(0, 2) == 0;

        // Instantiate the lightning strike prefab at the player's position
        GameObject lightning = Instantiate(lightningStrikePrefab, player.position, Quaternion.identity);

        // Get the sprite renderer of the lightning prefab
        SpriteRenderer lightningSpriteRenderer = lightning.GetComponent<SpriteRenderer>();

        // Set the flipX property of the sprite renderer to visually flip the lightning
        lightningSpriteRenderer.flipX = shouldFlipHorizontally;

        StartCoroutine(LightningStrike());
    }

    private IEnumerator LightningStrike()
    {
        // Wait for a brief moment
        yield return new WaitForSeconds(0.2f);

        // Do whatever damage or effects you want to apply to the player here
        // For example: player.TakeDamage(damageAmount);

        // Reset the isAttacking flag so the wizard can attack again if the player is still in range
        isAttacking = false;
    }
}