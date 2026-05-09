using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [HideInInspector]
    public Vector3 movementVector;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    [SerializeField] public int speed = 3;

    public Animator animator;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer RemyspriteRenderer;

    private void Awake()
    {
        int addedSpeed = PlayerPrefs.GetInt("speed");
        speed = speed += addedSpeed;
        PlayerPrefs.SetInt("originalSpeed", speed);
        PlayerPrefs.Save();
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;
    }

    void Update()
    {
        if (RemyspriteRenderer != null)
        {
            RemyspriteRenderer.flipX = spriteRenderer.flipX;
        }

        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if (movementVector.sqrMagnitude > 1)
        {
            movementVector.Normalize();
        }

        if (movementVector.x != 0)
        {
            lastHorizontalVector = movementVector.x;
            if (lastHorizontalVector <= -1)
            {
                spriteRenderer.flipX = true;
            }
            else if (lastHorizontalVector >= 1)
            {
                spriteRenderer.flipX = false;
            }
        }
        if (movementVector.y != 0)
        {
            lastVerticalVector = movementVector.y;
        }
        animator.SetFloat("Horizontal", movementVector.x);
        animator.SetFloat("Vertical", movementVector.y);
        animator.SetFloat("Speed", movementVector.sqrMagnitude);



        movementVector *= speed;

        rgbd2d.velocity = movementVector;
    }
}