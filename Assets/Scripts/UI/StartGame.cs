using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Image shopLock;
    public AudioSource error;

    private void Update()
    {
        if (shopLock != null)
        {
            if (ShopGameManager.Instance.dataContainer.shopUnlocked >= 1)
            {
                shopLock.enabled = false;
            }
            else
            {
                shopLock.enabled = true;
            }
        }

    }
    public void StartGameplay()
    {
        LoadData();
        if (ShopGameManager.Instance.dataContainer.tutorial != 0)
        {
            SceneManager.LoadScene("Villain1");
            Time.timeScale = 1f;
        }
        else if (ShopGameManager.Instance.dataContainer.tutorial == 0)
        {
            SceneManager.LoadScene("Tutorial");
            Time.timeScale = 1f;
        }

    }
public void Shop()
{
    LoadData();

    // If player has a key OR shop is already unlocked
    if (ShopGameManager.Instance.dataContainer.keys >= 1 || ShopGameManager.Instance.dataContainer.shopUnlocked >= 1)
    {
        // If shop is locked but player has a key, unlock it first
        if (ShopGameManager.Instance.dataContainer.shopUnlocked == 0)
        {
            shopLock.enabled = false;
            print("unlocked");

            ShopGameManager.Instance.dataContainer.shopUnlocked++;
            ShopGameManager.Instance.dataContainer.keys -= 1;

            SaveData();
        }

        SceneManager.LoadScene("Shop");
        Time.timeScale = 1f;
    }
    else
    {
        // Shop is locked AND player has no key
        if (error != null)
        {
            error.Play();
        }
    }
}

    public void Quit()
    {
        Application.Quit();
    }
    private void LoadData()
    {
        ShopGameManager.Instance.LoadData();
    }

    private void SaveData()
    {
        ShopGameManager.Instance.SaveData();
    }
}
