using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMoveJoystick playerMoveJoystick;
    PlayerMovement playerMovement;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);
    private Color originalColour;

    public bool usingJoystick;
    

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerMoveJoystick = GetComponentInParent<PlayerMoveJoystick>();
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        Character character = FindObjectOfType<Character>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Enemy e = colliders[i].GetComponent<Enemy>();
            if(e != null)
            {
                int addedDamage = PlayerPrefs.GetInt("addedDamage");
                PostDamage(weaponStats.damage + addedDamage+ character.damage, colliders[i].transform.position);
                e.TakeDamage(weaponStats.damage + addedDamage);
            }
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

 

    IEnumerator AttackProcess()
    {
        for (int i=0; i< weaponStats.numberOfAttacks; i++)
        {
                //if (playerMoveJoystick.lastHorizontalVector >= 0)
                //{
                //    rightWhipObject.SetActive(true);
                //    Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
                //    ApplyDamage(colliders);
                //}
                //else
                //{
                //    leftWhipObject.SetActive(true);
                //    Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
                //    ApplyDamage(colliders);
                //}



            if (playerMovement.lastHorizontalVector > 0)
            {
                rightWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);

            }
            else
            {
                leftWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }


            yield return new WaitForSeconds(0.3f);
        }
       
    }
}
