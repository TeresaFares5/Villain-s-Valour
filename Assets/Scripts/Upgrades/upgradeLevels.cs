using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeLevels : MonoBehaviour
{
    public List<GameObject> level;

    public void UpgradeGot()
    {
        level[0].SetActive(true);
        level.RemoveAt(0);
    }
}
