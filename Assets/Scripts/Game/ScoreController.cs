using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI topScore;
    [SerializeField] TextMeshProUGUI lastScore;

    [SerializeField] GameObject scorePanels;

    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }

    private void Awake()
    {
        ScoreText.text = "0";
        topScore.text = PlayerPrefs.GetInt("topScore").ToString();
        lastScore.text = PlayerPrefs.GetInt("lastScore").ToString();
    }

    public void IncreaseScore(int amount)
    {
        int score = int.Parse(ScoreText.text) + amount;
        ScoreText.text = score.ToString();
        PlayerPrefs.SetInt("score", score);

        GetComponent<IncreaseSpeedOverScore>().IncreaseSpeed();
    }

    public void SetTopScore()
    {
        PlayerPrefs.SetInt("lastScore", PlayerPrefs.GetInt("score"));
        PlayerPrefs.SetInt("score", 0);

        if (PlayerPrefs.GetInt("topScore") < PlayerPrefs.GetInt("lastScore"))
            PlayerPrefs.SetInt("topScore", PlayerPrefs.GetInt("lastScore"));

        CloudOnceServices.instance.SubmitScoreToLeaderboard("TheClassic", PlayerPrefs.GetInt("topScore"));
    }

    public void HideScore()
    {
        scorePanels.SetActive(false);
    }

}
