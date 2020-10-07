using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorAssignation : MonoBehaviour
{
    [SerializeField] ObstacleColorController colorController;
    [SerializeField] ChangeColorOverTime changeColor;

    private void OnEnable()
    {
        int index = Random.Range(0, changeColor.Colors.Length);
        GetComponentInParent<ObstaclesCollision>().Color = changeColor.ColorNames[index];

        colorController.Color = changeColor.Colors[index];
        Color particleColor = changeColor.Colors[index];
        particleColor.a = 0.6f;
        colorController.ColorParticles = particleColor;
    }
}
