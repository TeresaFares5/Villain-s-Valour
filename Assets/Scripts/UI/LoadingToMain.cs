using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingToMain : MonoBehaviour
{
    public int framesToWait = 150; // Number of frames to wait before changing the scene

    private int frameCount = 0;

    private void Update()
    {
        frameCount++;

        if (frameCount >= framesToWait)
        {
            ChangeToMainMenuScene();
        }
    }

    private void ChangeToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu"); // Load the "MainMenu" scene
    }
}