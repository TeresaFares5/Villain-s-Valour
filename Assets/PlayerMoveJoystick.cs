using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveJoystick:MonoBehaviour
{
    Rigidbody2D rgbd2d;
   
    public Vector3 movementVector;
   
    public float lastHorizontalVector;

    public float lastVerticalVector;

    [SerializeField] int speed = 3;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public Joystick joystickScript;
    public GameObject joystick;
    public GameObject joystickBG;

    [SerializeField] private Transform weaponContainer;

    private void Awake()
    {
        joystickScript = FindObjectOfType<Joystick>();
        joystick = joystickScript.joystick;
        joystickBG = joystickScript.joystickBG;


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
        movementVector = new Vector3(joystickScript.joystickVec.x, joystickScript.joystickVec.y, 0);

        if (movementVector.sqrMagnitude > 1)
        {
            movementVector.Normalize();
        }

        if (movementVector.x != 0)
{
    lastHorizontalVector = movementVector.x;

    if (lastHorizontalVector < 0)
    {
        spriteRenderer.flipX = true;
        FlipWeaponLeft();
    }
    else if (lastHorizontalVector > 0)
    {
        spriteRenderer.flipX = false;
        FlipWeaponRight();
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

        rgbd2d.linearVelocity = movementVector;
    
    }
private void FlipWeaponLeft()
{
    Vector3 scale = weaponContainer.localScale;
    scale.x = -Mathf.Abs(scale.x);
    weaponContainer.localScale = scale;
}

private void FlipWeaponRight()
{
    Vector3 scale = weaponContainer.localScale;
    scale.x = Mathf.Abs(scale.x);
    weaponContainer.localScale = scale;
}
    
}
