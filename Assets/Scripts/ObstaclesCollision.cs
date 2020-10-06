using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesCollision : MonoBehaviour
{

    [SerializeField] ChangeColorOverTime changeColor;
    [SerializeField] GameObject player;
    [SerializeField] string color;

    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject particles;

    public string Color { get => color; set => color = value; }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(0.1f, 0.1f);

            if (DieOnCollision(Color))
            {
                //player.Die();
                Debug.Log("DIE");
            }
            else
            {
                Debug.Log("NOT DIE");

                obstacle.SetActive(false);
                particles.SetActive(true);

                /*
                UpdateScore();

                Invoke("Respawn", 1f);
                */
            }
        }
    }

}
