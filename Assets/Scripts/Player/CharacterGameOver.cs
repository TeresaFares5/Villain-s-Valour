using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    [SerializeField] GameObject weaponParent;
    [SerializeField] GameObject joystick;
    [SerializeField] Joystick mobileJoystick;
    [SerializeField] GameObject pauseIcon;
    [SerializeField] GameObject daggerIcon;

    [HideInInspector] public StageTimer stageTimer;

    public GameObject world;
    public GameObject enemy;

    private void Awake()
    {
        stageTimer = world.GetComponent<StageTimer>();
    }

    public void GameOver()
    {
        if (enemy != null)
            enemy.SetActive(false);

        GetComponent<PlayerMovement>().enabled = false;

        if (mobileJoystick != null)
            mobileJoystick.ResetJoystick();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (weaponParent != null)
            weaponParent.SetActive(false);

        if (joystick != null)
            joystick.SetActive(false);

        if (pauseIcon != null)
            pauseIcon.SetActive(false);

        if (daggerIcon != null)
            daggerIcon.SetActive(false);

        stageTimer.timeRunning = false;

        Time.timeScale = 0;
    }
}