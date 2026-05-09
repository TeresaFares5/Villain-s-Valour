using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShopData : MonoBehaviour
{
    public DataContainer dataContainer;


    private void Awake()
    {
        LoadData();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Reset()
    {
        ShopGameManager.Instance.dataContainer.coins = 0;
        ShopGameManager.Instance.dataContainer.addedHealth = 0;
        ShopGameManager.Instance.dataContainer.healthUpgrades = 0;
        ShopGameManager.Instance.dataContainer.addedDamage = 0;
        ShopGameManager.Instance.dataContainer.damageUpgrades = 0;
        ShopGameManager.Instance.dataContainer.speed = 0;
        ShopGameManager.Instance.dataContainer.speedUpgrades = 0;
        ShopGameManager.Instance.dataContainer.tutorial = 0;
        ShopGameManager.Instance.dataContainer.keys = 0;
        ShopGameManager.Instance.dataContainer.shopUnlocked = 0;
        ShopGameManager.Instance.dataContainer.strengthUnlocked = 0;
        ShopGameManager.Instance.dataContainer.speedUnlocked = 0;
        ShopGameManager.Instance.dataContainer.creditsBought = 0;
        ShopGameManager.Instance.dataContainer.creditsUnlocked = 0;
        SaveData();

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
