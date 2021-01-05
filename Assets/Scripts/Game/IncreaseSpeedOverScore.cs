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
                pooler.MinSpawnDelay -= 0.05f;
                pooler.MaxSpawnDelay -= 0.05f;
                pooler.Speed += 1f;
                break;
            case 60:
            case 70:
            case 80:
            case 90:
            case 100:
                pooler.Speed += .5f;
                if (pooler.MinSpawnDelay > 0.2)
                {
                    pooler.MinSpawnDelay -= 0.05f;
                }
                if (pooler.MaxSpawnDelay > 0.4)
                {
                    pooler.MaxSpawnDelay -= 0.05f;
                }
                break;
            default:
                break;
        }

        
    }
}
