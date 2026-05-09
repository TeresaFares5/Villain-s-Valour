using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color originalColor;
    public Color hoverColor;

    public Animator mainCameraAnimator; // Reference to the Main Camera's Animator

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        originalColor = textMesh.color;
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse entered the button.");
        textMesh.color = hoverColor;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse exited the button.");
        textMesh.color = originalColor;
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse clicked the button.");
        // Play the "PlayCamera" animation on the Main Camera
        mainCameraAnimator.SetTrigger("PlayButton");
        // Delay loading the scene by the animation duration
        Invoke("LoadGameScene", mainCameraAnimator.GetCurrentAnimatorClipInfo(0).Length);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("TestScene"); // Load the "TestScene" after the animation finishes
    }
}