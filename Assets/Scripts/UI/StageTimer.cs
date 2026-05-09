using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time;
    
    public bool timeRunning;

    private void Awake()
    {
        timeRunning = true;
        
    }
    private void Update()
    {
        
        if (timeRunning == true)
        {
            time += Time.deltaTime;
        }
        
        
    }
}
