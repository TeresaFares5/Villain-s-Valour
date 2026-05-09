using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponParent;
    [HideInInspector] public StageTimer stageTimer;
    public GameObject world;

    public GameObject enemy;
    private void Awake()
    {
        stageTimer = world.GetComponent<StageTimer>();
    }
    public void GameOver()
    {
        enemy.SetActive(false);
        GetComponent<PlayerMovement>().enabled = false;
        gameOverPanel.SetActive(true);
        weaponParent.SetActive(false);
        stageTimer.timeRunning = false;
    }
}
