using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ReactionGameController : MonoBehaviour
{
    [Header ("Objects")]
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject colorsPanel;
    [SerializeField] GameObject scores;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject[] colorObjects;
    [SerializeField] TextMeshProUGUI totalText;

    [Header("Scores")]
    [SerializeField] TextMeshProUGUI[] topScores;
    [SerializeField] TextMeshProUGUI[] lastScores;
    [SerializeField] TextMeshProUGUI topScoreTotal;
    [SerializeField] TextMeshProUGUI lastScoresTotal;


    [Header("Conf")]
    [SerializeField] [Range(0, 10)] float minColorDelay = 0;
    [SerializeField] [Range(0, 10)] float maxColorDelay = 10;
    [SerializeField] int repetitionsPerColor = 3;

    private List<GameObject> colorsToPlay;

    private int index = -1;

    private DateTime colorSpawnDate;
    private DateTime colorClickDate;


    void Awake()
    {
        ShowScores();
        PrepareLists();
    }

    private void PrepareLists()
    {
        colorsToPlay = new List<GameObject>();
        Dictionary<string, int> colorsCount = new Dictionary<string, int>();
        for (int i = 0; i < colorObjects.Length; i++)
        {
            colorsCount.Add(colorObjects[i].GetComponent<ReactionColor>().ColorName, 0);
        }

        int totalRepetitions = repetitionsPerColor * colorObjects.Length;

        GameObject candidateColor = null;
        ReactionColor candidateColorProps;

        for (int i = 0; i < totalRepetitions; i++)
        {
            do
            {
                int randomReps = UnityEngine.Random.Range(1, 6);
                for (int k = 0; k < randomReps; k++)
                {
                    candidateColor = colorObjects[UnityEngine.Random.Range(0, colorObjects.Length)];
                }
                candidateColorProps = candidateColor.GetComponent<ReactionColor>();
            } while (colorsCount[candidateColorProps.ColorName] == repetitionsPerColor);

            colorsCount[candidateColorProps.ColorName] = colorsCount[candidateColorProps.ColorName] + 1;
            colorsToPlay.Add(candidateColor);
        }
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
        scores.SetActive(false);
        StartCoroutine(ShowColor());
    }

    public void OnNoneColorClick()
    {
        gameOver.SetActive(true);
        colorsPanel.SetActive(false);
    }

    public void OnColorClick()
    {

        colorClickDate = DateTime.UtcNow;
        TimeSpan diff = colorClickDate - colorSpawnDate;

        colorsToPlay[index].GetComponent<ReactionColor>().SetTime(diff.TotalSeconds);
        colorsToPlay[index].SetActive(false);

        if (index == colorsToPlay.Count -1 )
        {
            endScreen.SetActive(true);
            double total = 0;
            double count = 0;
            ReactionColor reaction;
            for (int i = 0; i < colorObjects.Length; i++)
            {
                reaction = colorObjects[i].GetComponent<ReactionColor>();
                reaction.ShowTimes();
                total += reaction.GetTimesTotal();
                count += reaction.Times.Count;

                SetScores(reaction.ColorName, reaction.GetTimeMedia());
            }
            total = total / count;
            totalText.text = Math.Round(total, 4).ToString() + "s";

            SetTopScoreDouble(total);
        }
        else
        {
            StartCoroutine(ShowColor());
        }
    }


    IEnumerator ShowColor()
    {
        index++;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minColorDelay, maxColorDelay));
        colorsToPlay[index].SetActive(true);
        colorSpawnDate = DateTime.UtcNow;
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameMode_Reaction");
    }

    private void SetScores(string colorName, double score)
    {
        PlayerPrefs.SetString("reactionLast" + colorName + "Score", score.ToString());

        if(PlayerPrefs.GetString("reactionTop" + colorName + "Score") == "" || double.Parse(PlayerPrefs.GetString("reactionTop" + colorName + "Score")) < score)
        {
            PlayerPrefs.SetString("reactionTop" + colorName + "Score", score.ToString());
        }
    }

    private void ShowScores()
    {
        ReactionColor reaction;
        for (int i = 0; i < colorObjects.Length; i++)
        {
            reaction = colorObjects[i].GetComponent<ReactionColor>();

            string topScore = PlayerPrefs.GetString("reactionTop" + reaction.ColorName + "Score") != "" ? PlayerPrefs.GetString("reactionTop" + reaction.ColorName + "Score") : "0";
            string lastScore = PlayerPrefs.GetString("reactionLast" + reaction.ColorName + "Score") != "" ? PlayerPrefs.GetString("reactionLast" + reaction.ColorName + "Score") : "0";

            topScores[i].text = Math.Round(double.Parse(topScore), 4).ToString() + "s";
            lastScores[i].text = Math.Round(double.Parse(lastScore), 4).ToString() + "s";
        }

        string sTopScoreTotal = PlayerPrefs.GetString("reactionTopScore") != "" ? PlayerPrefs.GetString("reactionTopScore") : "0";
        string sLastScoreTotal = PlayerPrefs.GetString("reactionLastScore") != "" ? PlayerPrefs.GetString("reactionLastScore") : "0";

        topScoreTotal.text = Math.Round(double.Parse(sTopScoreTotal), 4).ToString() + "s";
        lastScoresTotal.text = Math.Round(double.Parse(sLastScoreTotal), 4).ToString() + "s";
    }

    private void SetTopScoreDouble(double lastScore)
    {
        PlayerPrefs.SetString("reactionLastScore", lastScore.ToString());

        if (PlayerPrefs.GetString("reactionTopScore") == "" || 
            double.Parse(PlayerPrefs.GetString("reactionTopScore")) < double.Parse(PlayerPrefs.GetString("reactionTopScore")))
            PlayerPrefs.SetString("reactionTopScore", PlayerPrefs.GetString("reactionLastScore"));

        PlayServices.instance.AddScoreToLeaderboard(PlayerPrefs.GetInt("reactionTopScore"), "Reaction");
    }

    public void ShowLeaderBoard()
    {
        PlayServices.instance.ShowLeaderboard("Reaction");
    }
}
