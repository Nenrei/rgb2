using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] GameObject deadParticles;
    [SerializeField] GameObject shape;
    [SerializeField] ScoreController score;

    public void Score()
    {
        anim.SetTrigger("hit");
        score.IncreaseScore(1);
    }

    public void Die()
    {
        deadParticles.SetActive(true);
        shape.SetActive(false);

        score.SetTopScore();

        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }


}
