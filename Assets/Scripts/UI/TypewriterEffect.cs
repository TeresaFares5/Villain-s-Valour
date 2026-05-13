using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string[] fullTexts;

    private TMP_Text tmpText;
    private int currentLayer = 0;
    private bool isTyping = true;

    public AudioClip typeSound;
    private AudioSource audioSource;

    public string sceneName;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();

        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(ShowText());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Instantly finish current text
                StopAllCoroutines();
                tmpText.text = fullTexts[currentLayer];
                isTyping = false;
            }
            else
            {
                currentLayer++;

                if (currentLayer < fullTexts.Length)
                {
                    tmpText.text = "";
                    StartCoroutine(ShowText());
                }
                else
                {
                    // Load next scene
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }

    IEnumerator ShowText()
    {
        isTyping = true;

        for (int i = 0; i < fullTexts[currentLayer].Length; i++)
        {
            tmpText.text = fullTexts[currentLayer].Substring(0, i + 1);

            if (i % 2 == 0 && typeSound != null)
            {
                audioSource.PlayOneShot(typeSound);
            }

            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
    }
}