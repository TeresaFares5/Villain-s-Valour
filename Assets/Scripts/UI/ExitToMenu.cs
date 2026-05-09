using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }  
    
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Villain1()
    {
        SceneManager.LoadScene("Villain1");
    }
    public void Villain2()
    {
        SceneManager.LoadScene("Villain2");
    }
}
