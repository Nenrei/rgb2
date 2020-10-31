using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorAssignation : MonoBehaviour
{
    [SerializeField] ObstacleColorController colorController;
    [SerializeField] ChangeColorOverTime changeColor;

    bool spawnGolden;
    ObstaclesPooler pooler;

    public bool SpawnGolden { get => spawnGolden; set => spawnGolden = value; }

    private void OnEnable()
    {
        if(pooler == null)
            pooler = transform.parent.parent.GetComponent<ObstaclesPooler>();

        if(pooler.PooledColors.Count >= 3)
        {
            //Debug.Log("Han salido mas de 3 " + pooler.PooledColors[0] + " seguidos");
            GetNewColor(changeColor.CurrColor);
        }
        else
        {
            GetNewColor(changeColor.GetRandomColor());
        }
    }

    void GetNewColor(int colorIndex)
    {
        if (spawnGolden)
        {
            colorIndex = changeColor.GoldIndex;
            spawnGolden = false;
        }

        GetComponentInParent<ObstaclesCollision>().Color = changeColor.ColorNames[colorIndex];

        colorController.Color = changeColor.Colors[colorIndex];

        Color particleColor = changeColor.Colors[colorIndex];
        particleColor.a = 0.6f;
        colorController.ColorParticles = particleColor;

        Color lightColor = changeColor.Colors[colorIndex];
        lightColor.a = 0.3f;
        colorController.ColorLight = lightColor;

        if(colorIndex != changeColor.CurrColor)
        {
            pooler.SetObstacleColor(GetComponentInParent<ObstaclesCollision>().Color);
        }
        else
        {
            pooler.PooledColors.Clear();
            //Debug.Log("Ha salido el color actual, se limpia la lista");
        }
    }
}
