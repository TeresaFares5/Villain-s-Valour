using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI inputScore;

    public UnityEvent<string, int> submitScoreEvent;

   public void SubmitScore()
{
    Debug.Log("Submit button clicked");

    string inputName = PlayerPrefs.GetString("Player Name", "Player");

    if (!int.TryParse(inputScore.text, out int score))
    {
        Debug.LogError("Invalid score text: " + inputScore.text);
        return;
    }

    Debug.Log("Submitting score: " + inputName + " - " + score);

    submitScoreEvent.Invoke(inputName, score);
}
}