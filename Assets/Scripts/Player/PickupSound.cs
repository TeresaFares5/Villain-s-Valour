using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSound : MonoBehaviour
{
    public AudioClip heartSound;
    public AudioClip coinSound;
    public AudioClip gemSound;

    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heart"))
        {
            // Play heart sound 
            audioSource.PlayOneShot(heartSound);
        }
        else if (collision.CompareTag("Coin"))
        {
            // Play coin sound 
            audioSource.PlayOneShot(coinSound);
        }
        else if (collision.CompareTag("Gem"))
        {
            // Play gem sound 
            audioSource.PlayOneShot(gemSound);
        }
    }
}

