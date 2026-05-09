using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkulls : MonoBehaviour
{
    public GameObject skullPrefab; // Prefab of the skull sprite
    public Transform skullCenter; // The reference transform for the center of the skulls

    private GameObject[] skulls; // Array to store skull instances
    public int upgradeLevel = 0; // Current upgrade level

    private const int MaxUpgrades = 4; // Total number of upgrades

    // Arrays to store the rotation duration, reappear time, and rotation speed for each upgrade level
    private readonly float[] rotationDurations = { 5f, 7f, 9f, float.PositiveInfinity }; // float.PositiveInfinity for the final level
    private readonly float[] reappearTimes = { 5f, 4f, 3f, 2f };
    private readonly float[] rotationSpeeds = { 50f, 70f, 120f, 180f }; // Updated as per your request

    Character character;
    private Coroutine continuousCycleCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the array to store the skulls
        skulls = new GameObject[MaxUpgrades];
        character = GetComponent<Character>();

        // Call the HandleUpgrade() function to activate the initial upgrade level skulls
        HandleUpgrade();

        // Start the coroutine for continuous cycle of disappearing and reappearing skulls
        continuousCycleCoroutine = StartCoroutine(ContinuousCycle());
        if (character.canUseSkulls == 0f)
        {
            foreach (GameObject skull in skulls)
            {
                if (skull != null)
                    skull.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        upgradeLevel = character.skullCount;

        // Check for the upgrade key press
        if (character.canUseSkulls >= 1f)
        {
            // Increment the upgrade level and ensure it stays within the limits
            upgradeLevel = (upgradeLevel + 1) % (MaxUpgrades + 1);

            // Handle the upgrade and rotation behavior
            HandleUpgrade();
            upgradeLevel = 1;

            // Reset the canUseSkulls value
            character.canUseSkulls = 0f;
        }

        RotateSkullsAroundPlayer();
    }

    // Function to rotate the skulls around the player
    private void RotateSkullsAroundPlayer()
    {
        for (int i = 0; i < upgradeLevel; i++)
        {
            if (skulls[i] != null && skulls[i].activeSelf)
            {
                // Calculate the rotation angle based on time and rotation speed
                float angle = Time.time * rotationSpeeds[upgradeLevel - 1] % 360f;

                // Calculate the spawn position based on the angle and the fixed radius (10 units)
                Vector3 offset = Quaternion.Euler(0f, 0f, angle + 360f / MaxUpgrades * i) * (Vector3.right * 10f);
                Vector3 spawnPosition = skullCenter.position + offset;

                // Update the skull's position to maintain the fixed radius
                skulls[i].transform.position = spawnPosition;
            }
        }
    }

    // Function to handle upgrades
    private void HandleUpgrade()
    {
        // Deactivate all existing skulls
        foreach (GameObject skull in skulls)
        {
            if (skull != null)
                Destroy(skull);
        }

        // Activate skulls based on the upgrade level
        skulls = new GameObject[MaxUpgrades];
        for (int i = 0; i < upgradeLevel; i++)
        {
            float angle = 360f / MaxUpgrades * i; // Equal distribution
            Vector3 spawnPosition = skullCenter.position + Quaternion.Euler(0f, 0f, angle) * (Vector3.right * 10f);
            skulls[i] = Instantiate(skullPrefab, spawnPosition, Quaternion.identity);
        }

        // Start the rotation for the active skulls
        foreach (GameObject skull in skulls)
        {
            if (skull != null)
                skull.SetActive(true);
        }
    }

    // Coroutine for continuous cycle of disappearing and reappearing skulls
    private IEnumerator ContinuousCycle()
    {
        while (true) // Loop the cycle continuously
        {
            // Wait for the rotation duration
            float rotationDuration = rotationDurations[Mathf.Min(upgradeLevel, MaxUpgrades - 1)];
            yield return new WaitForSeconds(rotationDuration);

            // Hide the active skulls after the rotation duration
            foreach (GameObject skull in skulls)
            {
                if (skull != null)
                    skull.SetActive(false);
            }

            // Wait for the reappear time
            float reappearTime = reappearTimes[Mathf.Min(upgradeLevel, MaxUpgrades - 1)];
            yield return new WaitForSecondsRealtime(reappearTime);

            // Reappear the skulls after the reappear time
            foreach (GameObject skull in skulls)
            {
                if (skull != null)
                    skull.SetActive(true);
            }
        }
    }
}
