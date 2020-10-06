using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialAssignation : MonoBehaviour
{
    [SerializeField] ChangeColorOverTime changeColor;

    private void OnEnable()
    {
        int index = Random.Range(0, changeColor.Materials.Length);
        GetComponent<MeshRenderer>().material = changeColor.Materials[index];
        GetComponentInParent<ObstaclesCollision>().Color = changeColor.ColorNames[index];
    }
}
