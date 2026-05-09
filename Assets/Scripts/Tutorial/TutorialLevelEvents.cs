using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelEvents : MonoBehaviour
{
    public DataContainer dataContainer;

    public TutorialUI tutorialUI;
    public GameObject lvl1Gems;

    public int number = 0;

    public GameObject character;
    TutorialEnemySpawn tutorialEnemySpawn;

    public GameObject swordButton;
    
    public GameObject upgradePanel;

    private void Awake()
    {
        LoadData();
        ShopGameManager.Instance.dataContainer.tutorial += 1;
        SaveData();
    }

    void Start()
    {
        tutorialUI = FindObjectOfType<TutorialUI>();
        tutorialEnemySpawn = FindObjectOfType<TutorialEnemySpawn>();
    }

    
    void Update()
    {
        if(!tutorialUI.isTyping && lvl1Gems != null && lvl1Gems.activeInHierarchy == false)
        {
            lvl1Gems.SetActive(true);
            Debug.Log("gems");
        }

        if(!lvl1Gems && number == 0)
        {
            number+=1;
            print("collided");
            Invoke("PlayText", 0.5f);
        }
    }
 
    public void Sword()
    {
        character.GetComponent<WeaponManager>().enabled = true;
        Invoke("Golem", 10);

    }

    public void DestroyUpgradePanel()
    {
        Destroy(upgradePanel);
    }

    public void UsingWand()
    {
        if (number == 1)
        {
            number += 1;
            Invoke("PlayText", 0.5f);
        }
    }

    public void Survive()
    {
        if(number == 2)
        {
            number += 1;
            Invoke("SpawnHarderEnemy", 10);
        }
    }
    public void SpawnHarderEnemy()
    {
        tutorialEnemySpawn.SpawnHarderEnemy();
        Invoke("PlayText", 0.5f);
    }

    public void Golem()
    {
        tutorialEnemySpawn.SpawnEnemy();
        PlayText();
        DestroyUpgradePanel();
        Invoke("SpawnHarderEnemy", 10);
    }

    public void PlayText()
    {
        if (tutorialUI.currentLayer < tutorialUI.fullTexts.Length)
        {
            tutorialUI.currentLayer++;
            tutorialUI.tmpText.text = "";
            tutorialUI.isTyping = true;
            tutorialUI.DisplayText();

        }
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
