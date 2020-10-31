using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController instance;
    [SerializeField] bool gameStarted;
    [SerializeField] GameObject pausaButton;
    [SerializeField] GameObject tutorial;

    public bool GameStarted { get => gameStarted; set => gameStarted = value; }
    public static GameController Instance { get => instance; set => instance = value; }

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


    public void StartGame()
    {
        GameStarted = true;
        StartCoroutine(GameObject.Find("SquarePooler").GetComponent<ObstaclesPooler>().WaitAndRespawn());
        GetComponent<ScoreController>().HideScore();
        
        tutorial.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausaButton.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pausaButton.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        PlayServices.instance.ShowLeaderboard("The Classic");
    }
}
