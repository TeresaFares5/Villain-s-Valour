using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseManualWeapon : MonoBehaviour
{
    public GameObject wand;
    public bool coolDownOver = true;
    public float timeLeft = 10.0f;

    public GameObject joystick;

    public Slider timerSlider;

    private void Start()
    {
        timerSlider.maxValue = timeLeft;
        timerSlider.value = timeLeft;
    }

    public void UseWand()
    {
        if (coolDownOver)
        {

            wand.SetActive(true);
            
            
            timeLeft = 10.0f;
            timerSlider.value = timeLeft;   
        }
    }

    private void Update()
    {
        if (wand.activeInHierarchy)
        {
            joystick.SetActive(false);
        }
        else if (!wand.activeInHierarchy)
        {
            joystick.SetActive(true);
        }

        if (!coolDownOver)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = timeLeft;
            if (timeLeft < 0)
            {
                coolDownOver = true;
            }
        }
    }
    public void StartTimer()
    {
        coolDownOver = false;  
    }
}
