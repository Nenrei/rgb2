﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOverTime : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color[] colors;
    [SerializeField] string[] colorNames;

    [Header("Changing Objects")]
    [Space(10)]
    [SerializeField] SpriteRenderer playerShape;
    [SerializeField] SpriteRenderer playerGlow;
    [SerializeField] ParticleSystem playerParticles;

    [Header("Options")]
    [SerializeField] float changeSpeed = 5;

    int currColor = 0;

    int prevColor = -1;
    bool changingColor;
    float changeTime;

    public int CurrColor { get => currColor; set => currColor = value; }
    public bool ChangingColor { get => changingColor; set => changingColor = value; }
    public float ChangeTime { get => changeTime; set => changeTime = value; }
    public Color[] Colors { get => colors; set => colors = value; }
    public string[] ColorNames { get => colorNames; set => colorNames = value; }

    void Start()
    {
        ChangeMaterial();
    }


    void FixedUpdate()
    {
        if (ChangingColor)
        {

            playerShape.color = LerpColor(playerShape);
            playerGlow.color = LerpColor(playerGlow);

            ParticleSystem.MainModule ma = playerParticles.main;
            ma.startColor = LerpColor(ma);


            ChangeTime += Time.deltaTime;

            if (playerShape.color == Colors[currColor])
            {
                ChangingColor = false;
                Invoke("ChangeMaterial", 4f);
                ChangeTime = 0;
            }
        }
    }

    Color LerpColor(SpriteRenderer originObject)
    {
        return Color.Lerp(originObject.color, Colors[currColor], changeSpeed*Time.deltaTime);
    }
    Color LerpColor(ParticleSystem.MainModule originObject)
    {
        return Color.Lerp(originObject.startColor.color, Colors[currColor], changeSpeed * Time.deltaTime);
    }

    void ChangeMaterial()
    {
        if (!ChangingColor)
        {
            prevColor = currColor;
            currColor = ChooseNewColor();
            ChangingColor = true;
            ChangeTime = 0;
        }
    }

    int ChooseNewColor()
    {
        int newColor = Random.Range(0, Colors.Length);
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
