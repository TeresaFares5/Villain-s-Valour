using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;
    [SerializeField] List<UpgradeData> lastUpgrades;
    List<UpgradeData> selecetedUpgrade;

    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;
    PassiveItems passiveItems;

    [SerializeField] List<UpgradeData> upgradesAvvailableOnStart;

    UpgradeDisplay upgradeDisplay;
    public upgradeLevels magnetUpgradeLevels;
    public upgradeLevels SwordUpgradeLevels;
    private bool armourAquired = false;

    [Header("Merges")]
    public bool cheeseRushPossible;
    [SerializeField] List<UpgradeData> cheeseRushMerge;
    public bool trianglesPossible;
    [SerializeField] List<UpgradeData> trianglesMerge;

    Character character;
    public int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
        upgradeDisplay = FindObjectOfType<UpgradeDisplay>();
        character = FindObjectOfType<Character>();
        cheeseRushPossible = false;
        trianglesPossible = false;
    }

    internal void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null)
        {
            return;
        }
        this.upgrades.AddRange(upgradesToAdd);
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrades(upgradesAvvailableOnStart);
    }

    private void Update()
    {
        if (character.hasCheese >=1 && character.hasRemy>=1)
        {
            cheeseRushPossible = true;
        }

        if (character.power>=1 && character.wisedom >=1 && character.courage >= 1)
        {
            trianglesPossible = true;
        }
    }
    public void AddExperince(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience,TO_LEVEL_UP);
    }

    public void Upgrade(int selecetedUpgradeID)
    {
        UpgradeData upgradeData = selecetedUpgrade[selecetedUpgradeID]; 

        if (acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                if(upgradeData.name == "Cursed Sword")
                {
                    if (SwordUpgradeLevels.level.Count != 0)
                    {
                        SwordUpgradeLevels.UpgradeGot();

                    }
                }
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                if (upgradeData.name == "Magnet")
                {
                    if (magnetUpgradeLevels.level.Count != 0)
                    {
                        magnetUpgradeLevels.UpgradeGot();

                    }
                }

                break;
            case UpgradeType.WeaponGet:

                weaponManager.AddWeapon(upgradeData.weaponData);
                if (upgradeDisplay.weaponSlotsParent.Count != 0)
                {
                    upgradeDisplay.DisplayWeapon(upgradeData.item);
                    upgradeDisplay.weaponSlotsParent.RemoveAt(0);
                }
                break;
            case UpgradeType.ItemGet:
                passiveItems.Equip(upgradeData.item);
                AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);

                if (upgradeData.name!= "Free Coins")
                {
                    if (upgradeData.name == "Heartless" && armourAquired == false)
                    {
                        if (upgradeDisplay.upgradeSlotsParent.Count != 0)
                        {
                            upgradeDisplay.DisplayIcon(upgradeData.icon);
                            upgradeDisplay.upgradeSlotsParent.RemoveAt(0);
                            armourAquired = true;
                            Debug.Log("armour");
                        }
                    }
                    else if (upgradeData.name != "Heartless")
                    {
                        if (upgradeDisplay.upgradeSlotsParent.Count != 0)
                        {
                            upgradeDisplay.DisplayIcon(upgradeData.icon);
                            upgradeDisplay.upgradeSlotsParent.RemoveAt(0);
                        }
                    }
                }
               
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selecetedUpgrade == null) { selecetedUpgrade = new List<UpgradeData>(); }
        selecetedUpgrade.Clear();
        selecetedUpgrade.AddRange(GetUpgrades(3));

        upgradePanel.OpenPanel(selecetedUpgrade);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);

        if (cheeseRushPossible)
        {
            AddUpgradesIntoTheListOfAvailableUpgrades(cheeseRushMerge);
            cheeseRushMerge = null;
        }

        if (trianglesPossible)
        {
            AddUpgradesIntoTheListOfAvailableUpgrades(trianglesMerge);
            trianglesMerge = null;
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            AddUpgradesIntoTheListOfAvailableUpgrades(lastUpgrades);
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }
}
