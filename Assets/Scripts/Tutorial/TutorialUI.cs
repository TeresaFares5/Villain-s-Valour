using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public float delay = 0.1f;
    public string[] fullTexts;
    private string currentText = "";
    public TMP_Text tmpText;
    public int currentLayer = 0;
    private int charCounter = 0;
    public AudioClip typeSound;
    public AudioSource audioSource;
    public bool isTyping = true;

    public GameObject gameManager;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        StartCoroutine(ShowText());
        audioSource = gameObject.AddComponent<AudioSource>();
        gameManager.GetComponent<TutorialLevelEvents>().enabled=(true);
    }

    public void DisplayText()
    {
        if (isTyping)
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        charCounter = 0;
        while (charCounter < fullTexts[currentLayer].Length && isTyping)
        {
            currentText = fullTexts[currentLayer].Substring(0, charCounter + 1);
            tmpText.text = currentText;

            if (charCounter % 2 == 0)
            {
                audioSource.PlayOneShot(typeSound);
            }

            charCounter++;
            
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;

    }
}
