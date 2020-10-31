using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class PlayServices : MonoBehaviour
{

    public static PlayServices instance;

    private Dictionary<String, String> leaderBoards;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LogIn();

        SetLeaderBoards();
    }

    private void LogIn()
    {
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => {
                if (success)
                {
                    GameObject.Find("GPStatus").GetComponent<TextMeshProUGUI>().text = "Google Play Status: OK";
                }
                else
                {
                    GameObject.Find("GPStatus").GetComponent<TextMeshProUGUI>().text = "Google Play Status: KO";
                }
            });
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
            GameObject.Find("GPStatus").GetComponent<TextMeshProUGUI>().text = exception.Message;
        }
    }

    private void SetLeaderBoards()
    {
        leaderBoards = new Dictionary<string, string>();

        leaderBoards.Add("The Classic", GPGSIds.leaderboard_leaderboard__the_classic);
    }

    public void AddScoreToLeaderboard(int score, string leaderboardName)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderBoards[leaderboardName], success => { });
        }
    }

    public void ShowLeaderboard(string leaderboardName)
    {
        if (Social.localUser.authenticated)
        {
            Debug.Log("Showing " + leaderboardName + " leaderboard, id: " + leaderBoards[leaderboardName]);
            PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderBoards[leaderboardName]);
        }
    }
    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Debug.Log("Showing all leaderboards");
            Social.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Debug.Log("Showing all Achievements");
            Social.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        if (Social.localUser.authenticated)
        {
            Debug.Log("Unlocking " + achievementID + " achievement, id: " + achievementID);
            Social.ReportProgress(achievementID, 100f, success => { });
        }
    }

}
