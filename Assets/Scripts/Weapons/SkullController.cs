using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    public GameObject skullPrefab;   // Reference to the skull prefab
    public float throwForce = 5f;    // The initial force to throw the skull
    public float horizontalForce = 2f; // The force to move the skull horizontally
    public float spinForce = 100f;    // The force to make the skull spin
    public int skullCount = 10;       // The number of skulls to throw in each batch
    public float throwInterval = 10f;  // The interval between batches of skull throwing
    

    Character character;

    private void Start()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (character.canUseSkulls >=1f)
        {
            StartThrowingSkulls();
            character.canUseSkulls = 0f;
        }
        
    }

    private void StartThrowingSkulls()
    {
        // Create a coroutine to repeatedly throw skulls
        StartCoroutine(ThrowSkullsCoroutine());

        //Invoke("StartThrowingSkulls", delay);
    }

    private IEnumerator ThrowSkullsCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < skullCount; i++)
            {
                // Instantiate the skull prefab
                GameObject skull = Instantiate(skullPrefab, transform.position, Quaternion.identity);

                // Determine the direction to throw (left or right)
                float throwDirection = transform.localScale.x > 0 ? 1f : -1f;

                // Get the skull's Rigidbody2D component
                Rigidbody2D skullRb = skull.GetComponent<Rigidbody2D>();

                // Apply the throw force
                skullRb.velocity = new Vector2(throwDirection * throwForce, throwForce);

                // Apply random horizontal force
                float randomHorizontalForce = Random.Range(-horizontalForce, horizontalForce);
                skullRb.AddForce(new Vector2(randomHorizontalForce, 0f), ForceMode2D.Impulse);

                // Apply spin force
                skullRb.angularVelocity = Random.Range(-spinForce, spinForce);

                yield return null; // Yielding null to allow physics updates between instantiating each skull
            }

            // Wait for the specified interval before throwing the next batch of skulls
            yield return new WaitForSecondsRealtime(throwInterval);
        }
    }
}
