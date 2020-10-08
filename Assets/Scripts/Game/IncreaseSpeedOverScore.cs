using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedOverScore : MonoBehaviour
{
    [SerializeField] ObstaclesPooler pooler;


    public void IncreaseSpeed()
    {
        int score = int.Parse(GetComponent<ScoreController>().ScoreText.text);

        switch (score)
        {
            case 5:
            case 10:
            case 20:
            case 30:
            case 40:
            case 50:
                pooler.Speed += 1f;
                break;
            case 60:
            case 70:
            case 80:
            case 90:
            case 100:
                pooler.Speed += .5f;
                break;
            default:
                break;
        }

        
    }
}
