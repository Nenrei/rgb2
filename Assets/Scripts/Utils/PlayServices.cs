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
    private Dictionary<String, String> achievements;

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

        SetLeaderBoardsAndArchievements();
    }

    private void LogIn()
    {
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => {});
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }

    private void SetLeaderBoardsAndArchievements()
    {
        leaderBoards = new Dictionary<string, string>();
        leaderBoards.Add("The Classic", GPGSIds.leaderboard_leaderboard__the_classic);


        achievements = new Dictionary<string, string>();
        achievements.Add("Classic Red Lover", GPGSIds.achievement_classic_red_lover);
        achievements.Add("Classic Green Lover", GPGSIds.achievement_classic_green_lover);
        achievements.Add("Classic Blue Lover", GPGSIds.achievement_classic_blue_lover);

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
