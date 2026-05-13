using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    [Header("LootLocker")]
    [SerializeField] private string leaderboardKey = "leaderboard";
    [SerializeField] private int maxResults = 10;

    private bool sessionStarted;

    private void Start()
    {
        StartLootLockerSession();
    }

    private void StartLootLockerSession()
    {
        LootLockerSDKManager.StartGuestSession(response =>
        {
            if (response.success)
            {
                sessionStarted = true;
                Debug.Log("LootLocker connected");
                GetLeaderboard();
            }
            else
            {
                Debug.LogError("LootLocker session failed: " + response.text);
            }
        });
    }

    public void GetLeaderboard()
    {
        if (!sessionStarted)
        {
            Debug.LogWarning("LootLocker session not started yet.");
            return;
        }

        LootLockerSDKManager.GetScoreList(leaderboardKey, maxResults, 0, response =>
        {
            if (!response.success)
            {
                Debug.LogError("Get leaderboard failed: " + response.text);
                return;
            }

            ClearLeaderboardUI();

            int loopLength = Mathf.Min(response.items.Length, names.Count, scores.Count);

            for (int i = 0; i < loopLength; i++)
{
    names[i].text = response.items[i].player.name;
    scores[i].text = response.items[i].score.ToString();

    names[i].ForceMeshUpdate();
    scores[i].ForceMeshUpdate();
}
        });
    }

    public void SetLeaderBoardEntry(string username, int score)
    {
        if (!sessionStarted)
        {
            Debug.LogWarning("LootLocker session not started yet.");
            return;
        }

        if (string.IsNullOrWhiteSpace(username))
            username = "Player";

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score <= bestScore)
        {
            Debug.Log("Score not submitted because it is not higher than best score.");
            GetLeaderboard();
            return;
        }

        PlayerPrefs.SetInt("BestScore", score);
        PlayerPrefs.Save();

       LootLockerSDKManager.SetPlayerName(username, nameResponse =>
{
    if (!nameResponse.success)
    {
        Debug.LogError("Set name failed: " + nameResponse.text);
        return;
    }

    LootLockerSDKManager.SubmitScore("", score, leaderboardKey, response =>
    {
        if (response.success)
        {
            Debug.Log("Score submitted");
            GetLeaderboard();
        }
        else
        {
            Debug.LogError("Submit failed: " + response.text);
        }
    });
});
    }

    private void ClearLeaderboardUI()
    {
        for (int i = 0; i < names.Count; i++)
            names[i].text = "";

        for (int i = 0; i < scores.Count; i++)
            scores[i].text = "";
    }
}