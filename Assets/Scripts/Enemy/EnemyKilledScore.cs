using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyKilledScore : MonoBehaviour
{
    public int enemiesKilled;

    public TextMeshProUGUI enemiesKilledText1;
    public TextMeshProUGUI enemiesKilledText2;

    void Update()
    {
        string scoreText = enemiesKilled.ToString();

        enemiesKilledText1.text = scoreText;
        enemiesKilledText2.text = scoreText;
    }
}