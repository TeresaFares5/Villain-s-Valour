using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickupObject : MonoBehaviour,IPickUpObject
{
    [SerializeField] int healAmount;
    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnPickUp(Character character)
    {
        character.Heal(healAmount);
    }
    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
