using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public float delay = 0.1f;
    public string[] fullTexts;
    private string currentText = "";
    private TMP_Text tmpText;
    private int charCounter = 0;
    public AudioClip typeSound;
    public AudioSource audioSource;
    public float displayDuration = 10f;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShowRandomText());
    }

    IEnumerator ShowRandomText()
    {
        int randomIndex = Random.Range(0, fullTexts.Length); 
        string currentLayerText = fullTexts[randomIndex];
        charCounter = 0;

        while (charCounter < currentLayerText.Length)
        {
            currentText = currentLayerText.Substring(0, charCounter + 1);
            tmpText.text = currentText;

            if (charCounter % 2 == 0)
            {
                audioSource.PlayOneShot(typeSound);
            }

            charCounter++;

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(displayDuration);

        
        currentText = "";
        tmpText.text = currentText;
    }
}


