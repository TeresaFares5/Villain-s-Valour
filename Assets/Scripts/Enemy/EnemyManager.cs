using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public int maxObjectCount = 150;
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    

    public int addedDamage;
    public int addedHealth;

    [Header("Golem")]
    [SerializeField] GameObject golem;
    [SerializeField] int numberOfGolem = 01;
    public float delay = 1f;

    [Header("Knight")]
    [SerializeField] GameObject knight;
    [SerializeField] int numberOfKnights = 01;
    public bool spawningKnights=false;
    public float longerDelay = 2f;

    [Header("Fairies")]
    [SerializeField] GameObject Fairies;
    [SerializeField] int numberOfFairies = 01;
    public bool spawningFairies = false;
    public float delayForFairies = 3f;
    public Vector3 prefabSize;
    
    [Header("Archer")]
    [SerializeField] GameObject Archer;
    [SerializeField] int numberOffArcher = 01;
    public bool spawningArcher = false;
    public float delayForArcher = 3f;
    

    [Header("Other stats")]
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer = 0f;
    [SerializeField] GameObject player;
    public float decreaseInterval = 20f; 
    public float numberToDecrease = -0.1f;
    
    private void Awake()
    {
        SpawnEnemy();
        
    }

    private void Update()
    {
        if (delay >0.2)
        {
            spawnTimer += Time.deltaTime; // Increase the timer with the elapsed time
            if (spawnTimer >= decreaseInterval)
            {
                // Increase the number
                delay += numberToDecrease;

                // Reset the timer by subtracting the increase interval multiple times
                spawnTimer -= decreaseInterval * (int)(spawnTimer / decreaseInterval);
               
                addedDamage++;
                addedHealth++;
                    
                
                
            }
        }
        if (delay <= 0.8 && !spawningKnights)
        {
            SpawnHarderEnemy();
            spawningKnights = true;
        }
        
        if (delay <= 0.5 && !spawningFairies)
        {
            SpawnFairy();
            SpawnArcher();
            spawningFairies = true;
            spawningArcher = true;
        }

        if (spawnedEnemies.Count > maxObjectCount)
        {
            GameObject oldestObject = spawnedEnemies[0];
            spawnedEnemies.RemoveAt(0);
            Destroy(oldestObject);
            print("Destroyed");
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
            spawnedEnemies.Add(easiestEnemy);
            easiestEnemy.GetComponent<Enemy>().SetTarget(player);
            easiestEnemy.transform.parent = transform;
            

        }
        Invoke("SpawnEnemy", delay);
    }
    public void SpawnHarderEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        for (int i = 0; i < numberOfKnights; i++) 
        {
            GameObject harderEnemies = Instantiate(this.knight);
            harderEnemies.transform.position = position;
            spawnedEnemies.Add(harderEnemies);
            harderEnemies.GetComponent<Enemy>().SetTarget(player);
            harderEnemies.transform.parent = transform;
            

        }
        Invoke("SpawnHarderEnemy", longerDelay);
    }
    
    public void SpawnFairy()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        for (int i = 0; i < numberOfFairies; i++) 
        {
            GameObject fairy = Instantiate(this.Fairies);
            fairy.transform.localScale = prefabSize;
            fairy.transform.position = position;
            spawnedEnemies.Add(fairy);
            fairy.GetComponent<Enemy>().SetTarget(player);
            fairy.transform.parent = transform;


        }
        Invoke("SpawnFairy", delayForFairies);
    }
    public void SpawnArcher()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        for (int i = 0; i < numberOffArcher; i++) 
        {
            GameObject archer = Instantiate(this.Archer);
            archer.transform.localScale = prefabSize;
            archer.transform.position = position;
            spawnedEnemies.Add(archer);
            archer.GetComponent<Enemy>().SetTarget(player);
            archer.transform.parent = transform;


        }
        Invoke("SpawnArcher", delayForArcher);
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if(UnityEngine.Random.value > 0.5f)
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
