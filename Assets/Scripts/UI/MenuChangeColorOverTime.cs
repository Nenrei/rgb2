using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuChangeColorOverTime : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color[] colors;
    [SerializeField] Color[] colorsOpaque;

    [Header("Changing Objects")]
    [SerializeField] Image[] images;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] ParticleSystem[] buttonParticles;


    [Header("Options")]
    [SerializeField] float changeSpeed = 5;

    int currColor = 0;

    int prevColor = -1;
    bool changingColor;
    float changeTime;

    int goldIndex = 3;

    public int CurrColor { get => currColor; set => currColor = value; }
    public bool ChangingColor { get => changingColor; set => changingColor = value; }
    public float ChangeTime { get => changeTime; set => changeTime = value; }
    public Color[] Colors { get => colors; set => colors = value; }
    public int GoldIndex { get => goldIndex; set => goldIndex = value; }

    void Start()
    {
        colorsOpaque = (Color[]) colors.Clone();

        for (int i = 0; i < colorsOpaque.Length; i++)
        {
            Color newC = colorsOpaque[i];
            newC.a = 1;
            colorsOpaque[i] = newC;
        }
        ChangeMaterial();
    }


    void FixedUpdate()
    {
        if (ChangingColor)
        {
            foreach(ParticleSystem ps in buttonParticles)
            {
                ParticleSystem.MainModule ma = ps.main;
                ma.startColor = LerpColor(ma);
            }

            foreach (Image im in images)
            {
                Color newImageColor = LerpColor(im);
                newImageColor.a = 1;
                im.color = newImageColor;
            }

            foreach (TextMeshProUGUI txt in texts)
            {
                Color newTxtColor = LerpColor(txt);
                newTxtColor.a = 1;
                txt.color = newTxtColor;
            }

            ChangeTime += Time.deltaTime;

            if (images[0].color == colorsOpaque[currColor])
            {
                ChangingColor = false;
                Invoke("ChangeMaterial", 4f);
                ChangeTime = 0;
            }
        }
    }

    Color LerpColor(ParticleSystem.MainModule originObject)
    {
        return Color.Lerp(originObject.startColor.color, Colors[currColor], changeSpeed * Time.deltaTime);
    }
    Color LerpColor(Image originObject)
    {
        return Color.Lerp(originObject.color, colorsOpaque[currColor], changeSpeed * Time.deltaTime);
    }
    Color LerpColor(TextMeshProUGUI originObject)
    {
        /*Color newColor = Color.Lerp(originObject.color, colorsOpaque[currColor], changeSpeed * Time.deltaTime);
        float H, S, V;
        Color.RGBToHSV(newColor, out H, out S, out V);
        V = 0.3f;
        Color newNewColor = Color.HSVToRGB(H, S, V);
        return newNewColor;*/
        return Color.Lerp(originObject.color, colorsOpaque[currColor], changeSpeed * Time.deltaTime);
    }

    void ChangeMaterial()
    {
        if (!ChangingColor)
        {
            prevColor = currColor;
            currColor = ChooseNewColor();
            ChangingColor = true;
            ChangeTime = 0;
            changeSpeed = Random.Range(2, 5);
        }
    }

    int ChooseNewColor()
    {
        int newColor = Random.Range(0, Colors.Length);

        if (currColor == newColor || goldIndex == newColor)
        {
            newColor = ChooseNewColor();
        }
        return newColor;
    }

    public int GetRandomColor()
    {
        int newColor = Random.Range(0, Colors.Length);

        if (goldIndex == newColor)
        {
            newColor = GetRandomColor();
        }
        return newColor;
    }

}
