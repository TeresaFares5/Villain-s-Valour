using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] DataContainer dataContainer;


    [SerializeField] int amount;

    private void Awake()
    {
        PlayerPrefs.SetInt("keys", dataContainer.keys);
        int keysAquired = PlayerPrefs.GetInt("keys");
    }
    public void OnPickUp(Character character)
    {
        int keysAquired = PlayerPrefs.GetInt("keys");
        dataContainer.keys += amount;
        PlayerPrefs.SetInt("keys", dataContainer.keys);
        PlayerPrefs.Save();
    }

}
