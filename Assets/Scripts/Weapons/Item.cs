using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class ItemStats
{
    public int armour;
    public int coins;
    public float magnetRadiusAddition;
    public float canUseManualWeapon;
    public float canUseSkulls;
    public int skullCount;
    public int hasRemy;
    public int hasCheese;
    public int cheeseRush;
    public int power;
    public int triangleOfPower;
    public int wisdom;
    public int triangleOfWisdom;
    public int courage;
    public int triangleOfCourage;
    public int SacredTriangle;
    public int eldestWand;
    public int nobody;
    public int hasNobody;
    public int hasStrangeHead;
    public int iceShards;

    internal void Sum(ItemStats stats)
    {
        armour += stats.armour;
        coins += stats.coins;
        magnetRadiusAddition += stats.magnetRadiusAddition;
        canUseManualWeapon += stats.canUseManualWeapon;
        canUseSkulls += stats.canUseSkulls;
        skullCount += stats.skullCount;
        hasRemy += stats.hasRemy;
        hasCheese += stats.hasCheese;
        cheeseRush += stats.cheeseRush;
        power += stats.power;
        triangleOfPower += stats.triangleOfPower;
        wisdom += stats.wisdom;
        triangleOfWisdom += stats.triangleOfWisdom;
        courage += stats.courage;
        triangleOfCourage += stats.triangleOfCourage;
        SacredTriangle += stats.SacredTriangle;
        eldestWand += stats.eldestWand;
        nobody += stats.nobody;
        hasNobody += stats.hasNobody;
        hasStrangeHead += stats.hasStrangeHead;
        iceShards += stats.iceShards;
    }
    
}
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public ItemStats stats;
    public List<UpgradeData> upgrades;


    public void Init(string Name)
    {
        this.Name = Name;
        stats = new ItemStats();
        upgrades = new List<UpgradeData>();
    }

    public void Equip(Character character)
    {
        Level level = FindObjectOfType<Level>();
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>(); 
        stats.armour = Mathf.RoundToInt(character.currentHp * 0.1f);
        character.armour += stats.armour;
        character.coins.Add(stats.coins);
        character.colliderSize += stats.magnetRadiusAddition;
        character.canUseManualWeapon += stats.canUseManualWeapon;
        character.canUseSkulls += stats.canUseSkulls;
        character.skullCount += stats.skullCount;
        character.hasRemy += stats.hasRemy;
        character.hasCheese += stats.hasCheese;
        character.cheeseRush += stats.cheeseRush;
        character.power += stats.power;
        character.damage += stats.triangleOfPower;
        character.wisedom += stats.wisdom;
        stats.triangleOfWisdom = Mathf.RoundToInt(level.experience * 0.1f);
        level.experience += stats.triangleOfWisdom;
        character.courage += stats.courage;
        playerMovement.speed += stats.triangleOfCourage;
        character.hasSacredTriangle += stats.SacredTriangle;
        character.eldestWand += stats.eldestWand;
        stats.nobody = Mathf.RoundToInt(character.currentHp * 0.1f);
        if (character.currentHp < 450)
        {
            character.currentHp += stats.nobody;
            character.hasNobody += stats.hasNobody;
        }
        character.hasStrangeHead += stats.hasStrangeHead;
        character.hasIceShards += stats.iceShards;
        

    }
    public void UnEquip(Character character)
    {
        character.armour -= stats.armour;
        character.colliderSize -= stats.magnetRadiusAddition;
        character.canUseManualWeapon -= stats.canUseManualWeapon;
        character.canUseSkulls -= stats.canUseSkulls;
        character.skullCount -= stats.skullCount;
        character.hasRemy -= stats.hasRemy;
        character.hasCheese -= stats.hasCheese;
        character.cheeseRush -= stats.cheeseRush;
    }
}
