//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemySpawnControl : MonoBehaviour
//{
//    public GameObject enemies;

//    // Update is called once per frame
//    void Update()
//    {
//        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
//        int numberOfEnemies = Enemies.Length;
//        if (numberOfEnemies >= 100)
//        {
//            enemies.GetComponent<EnemyManager>().enabled = false;
//        }
//        else if(numberOfEnemies < 100)
//        {
//            enemies.GetComponent<EnemyManager>().enabled = true;
//        }
//    }
//}
