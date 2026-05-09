using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemySpawn : MonoBehaviour
{
    [Header("Golem")]
    [SerializeField] GameObject golem;
    [SerializeField] int numberOfGolem = 01;
    public float delay = 1f;
    
    [Header("Knight")]
    [SerializeField] GameObject Knight;
    [SerializeField] int numberOfKnight = 01;
    public float delayForKnight = 1f;

    [Header("Other stats")]
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer = 0f;
    [SerializeField] GameObject player;
    public float decreaseInterval = 20f;
    public float numberToDecrease = -0.1f;


    // Update is called once per frame
    void Update()
    {
        if (delay > 0.2)
        {
            spawnTimer += Time.deltaTime; // Increase the timer with the elapsed time
            if (spawnTimer >= decreaseInterval)
            {
                // Increase the number
                delay += numberToDecrease;

                // Reset the timer by subtracting the increase interval multiple times
                spawnTimer -= decreaseInterval * (int)(spawnTimer / decreaseInterval);
            }
        }
    }
    public void SpawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        for (int i = 0; i < numberOfGolem; i++)
        {
            GameObject easiestEnemy = Instantiate(this.golem);
            easiestEnemy.transform.position = position;
            easiestEnemy.GetComponent<Enemy>().SetTarget(player);
            easiestEnemy.transform.parent = transform;


        }
        Invoke("SpawnEnemy", delay);
    }
    public void SpawnHarderEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        for (int i = 0; i < numberOfGolem; i++)
        {
            GameObject Knight = Instantiate(this.Knight);
            Knight.transform.position = position;
            Knight.GetComponent<Enemy>().SetTarget(player);
            Knight.transform.parent = transform;


        }
        Invoke("SpawnHarderEnemy", delay);
    }
    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;

        }
        position.z = 0;

        return position;
    }
}
