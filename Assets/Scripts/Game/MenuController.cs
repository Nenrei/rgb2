
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI version;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject buttonsPanel;


    private void Awake()
    {
        version.text = "v. " + Application.version;
    }

    public void StartClassicMode()
    {
        if(PlayerPrefs.GetInt("classicTutorial") == 0)
            SceneManager.LoadScene("Tutorial_GameMode_Classic");
        else
            SceneManager.LoadScene("GameMode_Classic");
    }

    public void StartReactionMode()
    {
        SceneManager.LoadScene("GameMode_Reaction");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        buttonsPanel.SetActive(false);
    }

    public void AcceptSettings()
    {
        settingsPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }

}
