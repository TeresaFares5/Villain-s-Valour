using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeDisplay : MonoBehaviour
{
    public List<Transform> upgradeSlotsParent;
    public List<Transform> weaponSlotsParent;
    public GameObject upgradeSlotPrefab; 
    public Item upgradeItem;
    
    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    public void DisplayUpgrade(Item upgrade)
    {
        if (upgradeSlotsParent.Count == 0)
        {
            return;
        }

        GameObject slot = Instantiate(upgradeSlotPrefab, upgradeSlotsParent[0]);
        Image slotImage = slot.GetComponent<Image>();

        if (upgrade.upgrades.Count > 0)
        {
            Sprite upgradeIcon = upgrade.upgrades[0].icon;
            slotImage.sprite = upgradeIcon;
        }
    }   

    public void DisplayIcon(Sprite sprite)
    {
        if (upgradeSlotsParent.Count == 0)
        {
            return;
        }

        GameObject slot = Instantiate(upgradeSlotPrefab, upgradeSlotsParent[0]);
        Image slotImage = slot.GetComponent<Image>();

        slotImage.sprite = sprite;
    }  

    public void DisplayWeapon(Item upgrade)
    {
        if (weaponSlotsParent.Count == 0)
        {
            return;
        }

        GameObject slot = Instantiate(upgradeSlotPrefab, weaponSlotsParent[0]);
        Image slotImage = slot.GetComponent<Image>();

        if (upgrade.upgrades.Count > 0)
        {
            Sprite upgradeIcon = upgrade.upgrades[0].icon;
            slotImage.sprite = upgradeIcon;
        }
    }

    public void DisplayWeaponIcon(Sprite sprite)
    {
        if (weaponSlotsParent.Count == 0)
        {
            return;
        }

        GameObject slot = Instantiate(upgradeSlotPrefab, weaponSlotsParent[0]);
        Image slotImage = slot.GetComponent<Image>();

        slotImage.sprite = sprite;
    }
}