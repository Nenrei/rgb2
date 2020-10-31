using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RDG;
using System;

public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera vCam;
    private float shakerTimer;

    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        shakerTimer = time;
        Vibration.Vibrate(Convert.ToInt64(time * 1000), -1, false);
    }

    private void Update()
    {
        if(shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            if(shakerTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0f;
                Vibration.Cancel();
            }
        }
    }
}
