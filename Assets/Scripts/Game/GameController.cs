using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] bool isTutorial;
    [SerializeField] bool gameStarted;
    [SerializeField] bool gamePaused;
    [SerializeField] GameObject pausaButtonPanel;
    [SerializeField] GameObject pausaButton;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject tutorialButtons;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject goldenCount;
    [SerializeField] GameObject touchPanel;
    [SerializeField] GameObject obstaclesPooler;

    [Header("Music")]
    [SerializeField] string gameMusic;

    public bool GameStarted { get => gameStarted; set => gameStarted = value; }
    public bool GamePaused { get => gamePaused; set => gamePaused = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (!SoundManager.instance.IsPlayingMusic(gameMusic))
        {
            SoundManager.instance.PlayMusic(gameMusic);
        }
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                UnPauseGame();
            }
            else
            {
                BackToMenu();
            }
        }
    }


    public void StartGame()
    {
        GameStarted = true;

        tutorial.SetActive(false);
        pausaButton.SetActive(true);

        if (isTutorial) return;

        StartCoroutine(GameObject.Find("SquarePooler").GetComponent<ObstaclesPooler>().WaitAndRespawn());
        GetComponent<ScoreController>().HideScore();
        tutorialButtons.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausaButtonPanel.SetActive(true);
        pausaButton.SetActive(false);
        if(!isTutorial)
            tutorial.SetActive(true);
        GamePaused = true;

        SoundManager.instance.PauseMusic();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pausaButtonPanel.SetActive(false);
        pausaButton.SetActive(true);
        tutorial.SetActive(false);
        GamePaused = false;


        SoundManager.instance.ResumeMusic();
    }

    public void ShowLeaderboard()
    {
        PlayServices.instance.ShowLeaderboard("The Classic");
    }

    public void ShowArchievements()
    {
        PlayServices.instance.ShowAchievements();
    }

    public void BackToMenu()
    {
        SoundManager.instance.StopMusic();
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        //SoundManager.instance.StopMusic();
        SceneManager.LoadScene("GameMode_Classic");
    }

    public void EndGame()
    {
        GameStarted = false;
        gameOver.SetActive(true);
        goldenCount.SetActive(false);
        touchPanel.SetActive(false); 
        obstaclesPooler.SetActive(false);
        pausaButton.SetActive(false);

        //SoundManager.instance.PlayMusic(gameMusic);
    }
}
