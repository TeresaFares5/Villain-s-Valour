using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name; // Use TextMeshProUGUI for TextMeshPro support
    [SerializeField] TextMeshProUGUI descriptionText; // Use TextMeshProUGUI for TextMeshPro support
    [SerializeField] TextMeshProUGUI quote; // Use TextMeshProUGUI for TextMeshPro support


    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        name.text = upgradeData.name;
        descriptionText.text = upgradeData.description; // Set the upgrade description text
        quote.text = upgradeData.quote;
    }

    internal void Clean()
    {
        icon.sprite = null;
        descriptionText.text = string.Empty; // Clear the upgrade description text
    }
}