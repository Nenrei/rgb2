
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public void StartClassicMode()
    {
        SceneManager.LoadScene("GameMode_Classic");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
