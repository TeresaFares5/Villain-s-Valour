using System.Collections;
using UnityEngine;

public class KnifeWeapon : WeaponBase
{
    PlayerMovement playerMovement;
    PlayerMoveJoystick playerMoveJoystick;

    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;

    [Header("Manual Cooldown")]
    public float baseCooldown = 5f;
    private float currentCooldown;
    private bool canAttack = true;

    private float lastFacingX = 1f;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerMoveJoystick = GetComponentInParent<PlayerMoveJoystick>();
    }

    private void Start()
    {
        if (ManualKnifeButton.Instance != null)
        {
            ManualKnifeButton.Instance.SetKnifeWeapon(this);
        }
    }

    private void Update()
    {
        UpdateCooldownFromStats();
        UpdateLastFacingDirection();
    }

    private void UpdateLastFacingDirection()
    {
        float directionX = 0f;

        if (playerMoveJoystick != null && playerMoveJoystick.lastHorizontalVector != 0)
        {
            directionX = playerMoveJoystick.lastHorizontalVector;
        }
        else if (playerMovement != null && playerMovement.lastHorizontalVector != 0)
        {
            directionX = playerMovement.lastHorizontalVector;
        }

        if (directionX > 0)
        {
            lastFacingX = 1f;
        }
        else if (directionX < 0)
        {
            lastFacingX = -1f;
        }
    }

    private void UpdateCooldownFromStats()
    {
        currentCooldown = baseCooldown;

        if (weaponStats != null)
        {
            currentCooldown -= weaponStats.timeToAttack;
        }

        if (currentCooldown < 0.5f)
        {
            currentCooldown = 0.5f;
        }
    }

    public void ManualAttackButton()
    {
        if (canAttack == false)
        {
            return;
        }

        Attack();
        StartCoroutine(CooldownRoutine());
    }

   private IEnumerator CooldownRoutine()
{
    canAttack = false;

    float remainingCooldown = currentCooldown;

    while (remainingCooldown > 0)
    {
        remainingCooldown -= Time.deltaTime;

        if (ManualKnifeButton.Instance != null)
        {
            ManualKnifeButton.Instance.UpdateCooldownUI(remainingCooldown, currentCooldown);
        }

        yield return null;
    }

    canAttack = true;

    if (ManualKnifeButton.Instance != null)
    {
        ManualKnifeButton.Instance.UpdateCooldownUI(0, currentCooldown);
    }
}

    public override void Attack()
    {
        Character character = FindObjectOfType<Character>();

        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject knife = Instantiate(knifePrefab);

            Vector3 newKnifePosition = transform.position;

            if (weaponStats.numberOfAttacks > 1)
            {
                newKnifePosition.y -= (spread * (weaponStats.numberOfAttacks - 1)) / 2;
                newKnifePosition.y += i * spread;
            }

            knife.transform.position = newKnifePosition;

            knifeProjectile knifeProjectile = knife.GetComponent<knifeProjectile>();
            knifeProjectile.SetDirection(lastFacingX, 0f);

            knifeProjectile.damage = weaponStats.damage + character.damage;
        }
    }
}