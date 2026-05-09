using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused;

    private void Update()
    {
        if (paused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused == false)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
        }
    }
    public void PauseGame()
    {
        paused = true;
    }

    public void UnPauseGame()
    {
        paused = false;
    }
}
