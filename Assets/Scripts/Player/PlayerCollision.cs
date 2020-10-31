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

    public int Lifes { get => lifes; set => lifes = value; }

    public void Score()
    {
        WeakShake();

        anim.SetTrigger("hit");
        score.IncreaseScore(1);
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

            StartCoroutine(ReturnToMenu());
        }
    }

    public void CollectGoldenObstacle()
    {
        WeakShake();

        Lifes++;
        StartCoroutine(goldenLives[Lifes - 1].WinLife());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }

    private void WeakShake()
    {
        camShake.ShakeCamera(2f, 0.25f);
    }

    private void StrongShake()
    {
        camShake.ShakeCamera(10f, 0.5f);
    }


}
