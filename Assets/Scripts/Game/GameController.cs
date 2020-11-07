using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance;
    [SerializeField] bool gameStarted;
    [SerializeField] bool gamePaused;
    [SerializeField] GameObject pausaButtonPanel;
    [SerializeField] GameObject pausaButton;
    [SerializeField] GameObject tutorial;

    public bool GameStarted { get => gameStarted; set => gameStarted = value; }
    public static GameController Instance { get => instance; set => instance = value; }
    public bool GamePaused { get => gamePaused; set => gamePaused = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
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
        StartCoroutine(GameObject.Find("SquarePooler").GetComponent<ObstaclesPooler>().WaitAndRespawn());
        GetComponent<ScoreController>().HideScore();
        
        tutorial.SetActive(false);
        pausaButton.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausaButtonPanel.SetActive(true);
        pausaButton.SetActive(false);
        tutorial.SetActive(true);
        GamePaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pausaButtonPanel.SetActive(false);
        pausaButton.SetActive(true);
        tutorial.SetActive(false);
        GamePaused = false;
    }

    public void ShowLeaderboard()
    {
        PlayServices.instance.ShowLeaderboard("The Classic");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
