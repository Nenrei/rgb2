
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public void StartClassicMode()
    {
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

}
