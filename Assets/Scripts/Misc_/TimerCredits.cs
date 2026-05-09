using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerCredits : MonoBehaviour
{

    public float targetTime = 60.0f;

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        SceneManager.LoadScene("Shop");
    }


}