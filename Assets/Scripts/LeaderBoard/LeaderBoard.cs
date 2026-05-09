using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    [Header("LootLocker")]
    [SerializeField] private string leaderboardKey = "Leaderboard";
    [SerializeField] private int maxResults = 10;

    private bool sessionStarted = false;

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
                Debug.LogError("LootLocker connection failed: " + response.errorData.message);
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
                Debug.LogError("Could not get leaderboard: " + response.errorData.message);
                return;
            }

            ClearLeaderboardUI();

            int loopLength = Mathf.Min(response.items.Length, names.Count, scores.Count);

            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = response.items[i].member_id;
                scores[i].text = response.items[i].score.ToString();
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
        {
            username = "Player";
        }

        LootLockerSDKManager.SubmitScore(username, score, leaderboardKey, response =>
        {
            if (response.success)
            {
                Debug.Log("Score submitted");
                GetLeaderboard();
            }
            else
            {
                Debug.LogError("Could not submit score: " + response.errorData.message);
            }
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