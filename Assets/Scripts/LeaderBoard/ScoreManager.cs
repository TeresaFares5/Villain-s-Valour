using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI inputScore;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        Debug.Log("Submit button clicked");

        string inputName = nameInput.text.Trim();

        if (string.IsNullOrWhiteSpace(inputName))
            inputName = "Player";

        string cleanScoreText = inputScore.text.Replace("\u200B", "").Trim();

        if (!int.TryParse(cleanScoreText, out int score))
        {
            Debug.LogError("Invalid score text: " + inputScore.text);
            return;
        }

        Debug.Log("Submitting score: " + inputName + " - " + score);

        submitScoreEvent.Invoke(inputName, score);
    }
}