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

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statusText;

    private bool sessionStarted;

    private void Start()
    {
        StartLootLockerSession();
    }

    private bool IsOffline()
    {
        return Application.internetReachability == NetworkReachability.NotReachable;
    }

    private void StartLootLockerSession()
    {
        if (IsOffline())
        {
            ShowOfflineMessage();
            return;
        }

        LootLockerSDKManager.StartGuestSession(response =>
        {
            if (response.success)
            {
                sessionStarted = true;

                HideOfflineMessage();

                Debug.Log("LootLocker connected");
                GetLeaderboard();
            }
            else
            {
                Debug.LogError("LootLocker session failed: " + response.text);
                ShowOfflineMessage();
            }
        });
    }

    public void GetLeaderboard()
    {
        Debug.Log("Checking leaderboard now. Internet: " + Application.internetReachability);

        if (IsOffline())
        {
            ShowOfflineMessage();
            return;
        }

        if (!sessionStarted)
        {
            StartLootLockerSession();
            return;
        }

        LootLockerSDKManager.GetScoreList(leaderboardKey, maxResults, 0, response =>
        {
            if (!response.success)
            {
                Debug.LogError("Get leaderboard failed: " + response.text);
                ShowOfflineMessage();
                return;
            }

            HideOfflineMessage();
            ClearLeaderboardUI();

            int loopLength = Mathf.Min(response.items.Length, names.Count, scores.Count);

            for (int i = 0; i < loopLength; i++)
            {
                if (response.items[i].player != null && !string.IsNullOrWhiteSpace(response.items[i].player.name))
                    names[i].text = response.items[i].player.name;
                else
                    names[i].text = "Player";

                scores[i].text = response.items[i].score.ToString();

                names[i].ForceMeshUpdate();
                scores[i].ForceMeshUpdate();
            }
        });
    }

    public void SetLeaderBoardEntry(string username, int score)
    {
        if (IsOffline())
        {
            ShowOfflineMessage();
            return;
        }

        if (!sessionStarted)
        {
            StartLootLockerSession();
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
                ShowOfflineMessage();
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
                    ShowOfflineMessage();
                }
            });
        });
    }

    private void ShowOfflineMessage()
    {
        ClearLeaderboardUI();

        if (statusText != null)
        {
            statusText.gameObject.SetActive(true);
            statusText.text = "Leaderboard is offline\nTry again later";
            statusText.ForceMeshUpdate();
        }
    }

    private void HideOfflineMessage()
    {
        if (statusText != null)
        {
            statusText.text = "";
            statusText.ForceMeshUpdate();
        }
    }

    private void ClearLeaderboardUI()
    {
        for (int i = 0; i < names.Count; i++)
        {
            names[i].text = "";
            names[i].ForceMeshUpdate();
        }

        for (int i = 0; i < scores.Count; i++)
        {
            scores[i].text = "";
            scores[i].ForceMeshUpdate();
        }
    }
}