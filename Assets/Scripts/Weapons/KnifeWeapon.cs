using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeWeapon : WeaponBase
{
    PlayerMovement playerMovement;
    PlayerMoveJoystick playerMoveJoystick;

    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerMoveJoystick = GetComponentInParent<PlayerMoveJoystick>();
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
                newKnifePosition.y -= (spread * (weaponStats.numberOfAttacks -1 )) / 2;
                newKnifePosition.y += i * spread;
            }

            knife.transform.position = newKnifePosition;
            knifeProjectile knifeProjectile = knife.GetComponent<knifeProjectile>();
            knifeProjectile.SetDirection(playerMovement.lastHorizontalVector, 0f);
            //knifeProjectile.SetDirection(playerMoveJoystick.lastHorizontalVector, 0f);

            knifeProjectile.damage = weaponStats.damage+character.damage;
        }
    }
}


