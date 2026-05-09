using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int damage = 1;
    Character character;

    private void Start()
    {
        character = FindObjectOfType<Character>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character"))
        {
            character.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
