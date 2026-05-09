using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyKilledScore : MonoBehaviour
{
    public int enemiesKilled;
    public TextMeshProUGUI enemiesKilledText;
    
    
    void Update()
    {
        enemiesKilledText.text = enemiesKilled.ToString();
    }
}
