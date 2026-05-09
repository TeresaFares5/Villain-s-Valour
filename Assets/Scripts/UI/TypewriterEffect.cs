using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string[] fullTexts;
    private string currentText = "";
    private TMP_Text tmpText;
    private int currentLayer = 0;
    private int charCounter = 0;
    public AudioClip typeSound;
    public AudioSource audioSource;
    private bool isTyping = true;

    public string sceneName;
    public GameObject usernameInput;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        StartCoroutine(ShowText());
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(ShowText());
                tmpText.text = fullTexts[currentLayer];
                isTyping = false;
            }
            else
            {
                currentLayer++;

                if (currentLayer < fullTexts.Length)
                {
                    tmpText.text = "";
                    isTyping = true;
                    DisplayText();
                }
                else
                {
                    usernameInput.SetActive(true);
                }
            }
        }
    }

    public void Enter()
    {
        SceneManager.LoadScene(sceneName);
    }

    void DisplayText()
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

