using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialAssignation : MonoBehaviour
{
    [SerializeField] ChangeColorOverTime changeColor;

    private void OnEnable()
    {
        int index = Random.Range(0, changeColor.Colors.Length);
        //GetComponent<Spri>().material = changeColor.Colors[index];
        GetComponentInParent<ObstaclesCollision>().Color = changeColor.ColorNames[index];
    }
}
