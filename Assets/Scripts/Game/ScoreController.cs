using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI topScore;
    [SerializeField] TextMeshProUGUI lastScore;

    [SerializeField] GameObject scorePanels;

    [SerializeField] string lastScoreKey;
    [SerializeField] string topScoreKey;
    [SerializeField] string leaderboard;

    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }

    private void Awake()
    {
        if (ScoreText)
        {
            ScoreText.text = "0";
        }
        topScore.text = PlayerPrefs.GetInt("topScore").ToString();
        lastScore.text = PlayerPrefs.GetInt("lastScore").ToString();
    }

    public void IncreaseScore(int amount)
    {
        if(ScoreText == null)
        {
            return;
        }

        int score = int.Parse(ScoreText.text) + amount;
        ScoreText.text = score.ToString();
        PlayerPrefs.SetInt("score", score);

        switch (SceneManager.GetActiveScene().name)
        {
            case "GameMode_Classic":
                GetComponent<IncreaseSpeedOverScore>().IncreaseSpeed();
                break;
        }
    }

    public void SetTopScore()
    {
        PlayerPrefs.SetInt(lastScoreKey, PlayerPrefs.GetInt("score"));
        PlayerPrefs.SetInt("score", 0);

        if (PlayerPrefs.GetInt(topScoreKey) < PlayerPrefs.GetInt(lastScoreKey))
            PlayerPrefs.SetInt(topScoreKey, PlayerPrefs.GetInt(lastScoreKey));

        PlayServices.instance.AddScoreToLeaderboard(PlayerPrefs.GetInt(topScoreKey), leaderboard);
    }

    public void HideScore()
    {
        scorePanels.SetActive(false);
    }

}
