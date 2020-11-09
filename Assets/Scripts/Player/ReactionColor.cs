using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ReactionColor : MonoBehaviour
{
    [SerializeField] string colorName;
    [SerializeField] List<double> times;

    [SerializeField] TextMeshProUGUI textColorName;
    [SerializeField] TextMeshProUGUI textColorTimes;
    [SerializeField] TextMeshProUGUI textColorMedia;

    public string ColorName { get => colorName; set => colorName = value; }
    public List<double> Times { get => times; set => times = value; }

    private void Awake()
    {
        Times = new List<double>();
    }

    public void SetTime(double newTime)
    {
        Times.Add(newTime);
    }

    public double GetTimeMedia()
    {
        return GetTimesTotal() / Times.Count;
    }

    public double GetTimesTotal()
    {
        double total = 0;
        for (int i = 0; i < Times.Count; i++)
        {
            total = total + Times[i];
        }

        return total;
    }

    public void ShowTimes()
    {
        textColorTimes.text = "";
        //textColorName.text = ColorName;
        for (int i = 0; i < Times.Count; i++)
        {
            textColorTimes.text += Math.Round(Times[i], 4).ToString() + "s" + "\r\n";
        }
        textColorMedia.text = Math.Round(GetTimeMedia(), 4).ToString() + "s";
    }
}
