using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CameraShake camShake;

    [SerializeField] Animator anim;
    [SerializeField] GameObject deadParticles;
    [SerializeField] GameObject shape;
    [SerializeField] ScoreController score;

    [SerializeField] LifesUI[] goldenLives;

    [SerializeField] int lifes;
    [SerializeField] int endGamesUntilAd = 3;

    private int ARCHIEVEMENT_PROCESS = 15;
    private string archievementColor = "";

    public int Lifes { get => lifes; set => lifes = value; }

    private void Awake()
    {
        if(PlayerPrefs.GetInt("classicEndGamesUntilAd") == 0)
        {
            PlayerPrefs.SetInt("classicEndGamesUntilAd", 1);
        }

        if (PlayerPrefs.GetInt("classicEndGamesUntilAd") == endGamesUntilAd)
        {
            AdMobManager.instance.RequestAndLoadInterstitialAd();
        }
    }
    public void Score(string collidedColor)
    {
        WeakShake();

        anim.SetTrigger("hit");
        score.IncreaseScore(1);

        ManageArchievements(collidedColor);
    }

    public void Die()
    {
        if (Lifes > 0)
        {
            WeakShake();

            StartCoroutine(goldenLives[Lifes-1].LooseLife());
            Lifes--;
        }
        else
        {
            StrongShake();

            deadParticles.SetActive(true);
            shape.SetActive(false);

            score.SetTopScore();

            StartCoroutine(ShowAds());

        }
    }

    public void CollectGoldenObstacle()
    {
        WeakShake();

        Lifes++;
        StartCoroutine(goldenLives[Lifes - 1].WinLife());
    }

    IEnumerator ShowAds()
    {
        yield return new WaitForSeconds(0.5f);

        GameController.instance.EndGame();

        if (PlayerPrefs.GetInt("classicEndGamesUntilAd") == endGamesUntilAd)
        {
            PlayerPrefs.SetInt("classicEndGamesUntilAd", 1);
            AdMobManager.instance.ShowInterstitialAd();
            Debug.Log("SHOW INTERSTICIAL");
        }
        else
        {
            PlayerPrefs.SetInt("classicEndGamesUntilAd", PlayerPrefs.GetInt("classicEndGamesUntilAd") + 1);
        }
    }

    private void WeakShake()
    {
        camShake.ShakeCamera(2f, 0.25f);
    }

    private void StrongShake()
    {
        camShake.ShakeCamera(10f, 0.5f);
    }

    private void ManageArchievements(string currColor)
    {
        if (archievementColor != currColor)
        {
            PlayerPrefs.SetInt("redLoverClassic", 0);
            PlayerPrefs.SetInt("greenLoverClassic", 0);
            PlayerPrefs.SetInt("blueLoverClassic", 0);
            archievementColor = currColor;
        }

        switch (currColor)
        {
            case "red":
                ColorArchievement("redLoverClassicArchievement", "redLoverClassic", "Classic Red Lover");
                break;
            case "green":
                ColorArchievement("greenLoverClassicArchievement", "greenLoverClassic", "Classic Green Lover");
                break;
            case "blue":
                ColorArchievement("blueLoverClassicArchievement", "blueLoverClassic", "Classic Blue Lover");
                break;
            default:
                break;
        }
    }

    private void ColorArchievement(string archievementControl, string archievementCount, string archievementId)
    {
        if (PlayerPrefs.GetInt(archievementControl) == 0)
        {
            int archProcess = PlayerPrefs.GetInt(archievementCount) + 1;
            PlayerPrefs.SetInt(archievementCount, archProcess);

            if (archProcess == ARCHIEVEMENT_PROCESS)
            {
                PlayerPrefs.SetInt(archievementControl, 1);
                PlayServices.instance.UnlockAchievement(archievementId);
                //Debug.Log("unlocked " + archievementId);
            }
        }
    }

}
