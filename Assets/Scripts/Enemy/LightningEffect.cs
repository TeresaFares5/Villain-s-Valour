using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    // The duration the lightning effect will be visible before being destroyed
    public float effectDuration = 2f;

    private void Start()
    {
        // Start the coroutine to destroy the lightning game object after the effectDuration
        StartCoroutine(DestroyAfterDuration());
    }

    private IEnumerator DestroyAfterDuration()
    {
        // Wait for the specified effectDuration
        yield return new WaitForSeconds(effectDuration);

        // Destroy the lightning game object after the effectDuration
        Destroy(gameObject);
    }
}
