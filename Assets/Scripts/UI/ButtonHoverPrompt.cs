using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverPrompt : MonoBehaviour
{
    public Image prompt;
    public Image strengthPrompt;
    public Image speedPrompt;
    public Image creditsPrompt;

    private void Start()
    {
        // Hide the prompt at the start
        if (prompt != null)
        {
            prompt.gameObject.SetActive(false);
        }
        if (strengthPrompt != null)
        {
            strengthPrompt.gameObject.SetActive(false);
        }
        if (speedPrompt != null)
        {
            speedPrompt.gameObject.SetActive(false);
        }
        if (creditsPrompt != null)
        {
            creditsPrompt.gameObject.SetActive(false);
        }
    }
    //for main Menu
    public void OnMouseEnter()
    {
        if (ShopGameManager.Instance.dataContainer.shopUnlocked == 0)
        {
            // Show the prompt when the mouse enters the button
            prompt.gameObject.SetActive(true);
        }
    }
    public void OnMouseExit()
    {
        // Hide the prompt when the mouse exits the button
        prompt.gameObject.SetActive(false);
    }

    //for speed
    public void OnMouseEnterSpeed()
    {
        if (ShopGameManager.Instance.dataContainer.speedUnlocked == 0)
        {
            // Show the prompt when the mouse enters the button
            speedPrompt.gameObject.SetActive(true);
        }
    }
    public void OnMouseExitSpeed()
    {
        // Hide the prompt when the mouse exits the button
        speedPrompt.gameObject.SetActive(false);
    }


    //for Strength
    public void OnMouseEnterStrength()
    {
        if (ShopGameManager.Instance.dataContainer.strengthUnlocked == 0)
        {
            // Show the prompt when the mouse enters the button
            strengthPrompt.gameObject.SetActive(true);
        }
    }
    public void OnMouseExitStrength()
    {
        // Hide the prompt when the mouse exits the button
        strengthPrompt.gameObject.SetActive(false);
    }
    public void OnMouseEnterCredts()
    {
        if (ShopGameManager.Instance.dataContainer.creditsUnlocked == 0)
        {
            // Show the prompt when the mouse enters the button
            creditsPrompt.gameObject.SetActive(true);
        }
    }
    public void OnMouseExitCredits()
    {
        // Hide the prompt when the mouse exits the button
        creditsPrompt.gameObject.SetActive(false);
    }

}
