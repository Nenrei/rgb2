using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOverTime : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Material[] materials;
    [SerializeField] string[] colorNames;

    [Header("Changing Objects")]
    [SerializeField] GameObject wallLeft;
    [SerializeField] GameObject wallRight;
    [Space(10)]
    [SerializeField] GameObject player;

    [Header("Options")]
    [SerializeField] float changeSpeed = 5;

    int currColor = 0;

    int prevColor = -1;
    bool changingColor;
    float changeTime;

    public int CurrColor { get => currColor; set => currColor = value; }
    public bool ChangingColor { get => changingColor; set => changingColor = value; }
    public float ChangeTime { get => changeTime; set => changeTime = value; }
    public Material[] Materials { get => materials; set => materials = value; }
    public string[] ColorNames { get => colorNames; set => colorNames = value; }

    void Start()
    {
        ChangeMaterial();
    }


    void FixedUpdate()
    {
        if (ChangingColor)
        {
            wallLeft.GetComponent<Renderer>().material.color = LerpColor(wallLeft);
            wallRight.GetComponent<Renderer>().material.color = LerpColor(wallLeft);
            player.GetComponent<Renderer>().material.color = LerpColor(wallLeft);

            ChangeTime += Time.deltaTime;

            if (player.GetComponent<Renderer>().material.color == Materials[currColor].color)
            {
                ChangingColor = false;
                Invoke("ChangeMaterial", 4f);
                ChangeTime = 0;
            }
        }
    }

    Color LerpColor(GameObject originObject)
    {
        return Color.Lerp(originObject.GetComponent<Renderer>().material.color, Materials[currColor].color, changeSpeed*Time.deltaTime);
    }

    void ChangeMaterial()
    {
        if (!ChangingColor)
        {
            if (prevColor != -1)
            {
                wallLeft.GetComponent<Animator>().SetTrigger("changeColor");
                wallRight.GetComponent<Animator>().SetTrigger("changeColor");
                player.GetComponent<Animator>().SetTrigger("changeColor");
            }

            prevColor = currColor;
            currColor = ChooseNewColor();
            ChangingColor = true;
            ChangeTime = 0;
        }
    }

    int ChooseNewColor()
    {
        int newColor = Random.Range(0, Materials.Length);
        if (currColor == newColor)
        {
            newColor = ChooseNewColor();
        }
        return newColor;
    }

    public string GetCurrentColor()
    {
        return ColorNames[currColor];
    }

}
