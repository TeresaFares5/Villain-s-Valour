using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public int enemiesKilled;
    [Header("Boss")]
    [SerializeField] GameObject boss;
    [SerializeField] int numberOfBoss = 01;
    public bool spawnedBoss = false;

    [Header("Other stats")]
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;


    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled >= 250)
        {
            SpawnBoss();
            enemiesKilled = 0;
        }
    }

    public void SpawnBoss()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        for (int i = 0; i < numberOfBoss; i++)
        {
            GameObject boss = Instantiate(this.boss);
            boss.transform.position = position;
            boss.GetComponent<Enemy>().SetTarget(player);
            boss.transform.parent = transform;

        }

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
