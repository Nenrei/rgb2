using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;

    [SerializeField] Toggle classicTutorialToggle;

    bool musicVolumeSaved;


    // Start is called before the first frame update
    void Start()
    {
        classicTutorialToggle.isOn = PlayerPrefs.GetInt("classicTutorial") == 0;

        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 0.5f);


        if (!PlayerPrefs.HasKey("effectsVolume"))
            PlayerPrefs.SetFloat("effectsVolume", 0.5f);

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");

        musicVolumeSaved = true;

        SetMusicVolume(musicSlider.value);
        SetSfxVolume(effectsSlider.value);
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (musicVolumeSaved)
        {
            SoundManager.instance.SetMusicVolume(sliderValue);
            //mainMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("musicVolume", sliderValue);
        }
    }

    public void SetSfxVolume(float sliderValue)
    {
        if (musicVolumeSaved)
        {
            SoundManager.instance.SetEffectsVolume(sliderValue);
            //mainMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("effectsVolume", sliderValue);
        }
    }

    public void SetClassicTutorial(bool showClassicTutorial)
    {
        PlayerPrefs.SetInt("classicTutorial", showClassicTutorial ? 0 : 1);
    }
}
