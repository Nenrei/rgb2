using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
using TMPro;

public class CloudOnceServices : MonoBehaviour
{
    public static CloudOnceServices instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }



    #region Leaderboards

    public void SubmitScoreToLeaderboard(string leaderboardName, int score)
    {
        switch (leaderboardName)
        {
            case "TheClassic":
                Leaderboards.TheClassic.SubmitScore(score);
                break;
            default:
                break;
        }
    }

    public void DisplayLeaderboard(string leaderboardName)
    {
        Debug.Log(leaderboardName);
        Cloud.Leaderboards.ShowOverlay(leaderboardName);
        GameObject.Find("Debug").GetComponent<TextMeshProUGUI>().text = leaderboardName;
        switch (leaderboardName)
        {
            case "TheClassic":
                //Cloud.Leaderboards.ShowOverlay(leaderboardName);
                break;
            default:
                break;
        }
    }

    #endregion

}
