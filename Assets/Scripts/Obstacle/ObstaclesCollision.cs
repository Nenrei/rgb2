using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesCollision : MonoBehaviour
{

    [SerializeField] ChangeColorOverTime changeColor;
    [SerializeField] PlayerCollision player;
    [SerializeField] string color;

    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject particles;

    public string Color { get => color; set => color = value; }

    private void OnEnable()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerCollision>();
    }

    public bool DieOnCollision(string obstacleColor)
    {
        if (changeColor == null)
            changeColor = GameObject.Find("GameController").GetComponent<ChangeColorOverTime>();

        if (changeColor.ChangingColor)
        {
            if (obstacleColor != changeColor.GetCurrentColor() && (changeColor.ChangeTime > 0.6f))
            {
                return true;
            }
        }
        else
        {
            if (obstacleColor != changeColor.GetCurrentColor())
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            //GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(0.1f, 0.1f);

            if (DieOnCollision(Color))
            {
                player.Die();
            }
            else
            {
                GetComponent<CircleCollider2D>().enabled = false;
                particles.SetActive(true);
                obstacle.SetActive(false);
                GetComponent<ObstacleMovement>().CanMove = false;

                StartCoroutine(ResetObstacle());

                player.Score();
            }
        }
    }

    IEnumerator ResetObstacle()
    {
        yield return new WaitForSeconds(0.5f);
        transform.localPosition = Vector3.zero;
        particles.SetActive(false);
        obstacle.SetActive(true);
        gameObject.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = true;
    }

}
