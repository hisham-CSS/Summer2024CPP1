using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public AudioMixer mixer;

    public Button backButton;
    
    public Slider masterVolSlider;
    public Slider sfxVolSlider;
    public Slider musicVolSlider;

    public TMP_Text masterVolSliderText;
    public TMP_Text sfxVolSliderText;
    public TMP_Text musicVolSliderText;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Settings;
        backButton.onClick.AddListener(JumpBack);

        if (masterVolSlider && masterVolSliderText) SetupSliderInformation(masterVolSlider, masterVolSliderText, "MasterVol");
        if (musicVolSlider && musicVolSliderText) SetupSliderInformation(musicVolSlider, musicVolSliderText, "MusicVol");
        if (sfxVolSlider && sfxVolSliderText) SetupSliderInformation(sfxVolSlider, sfxVolSliderText, "SFXVol");
    }

    void SetupSliderInformation(Slider mySlider, TMP_Text myText, string parameterName)
    {
        mySlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, myText, parameterName, mySlider));
        float newVal = (mySlider.value == 0.0f) ? -80.0f : 20.0f * Mathf.Log10(mySlider.value);
        mixer.SetFloat(parameterName, newVal);

        myText.text = (newVal == -80.0f) ? "0%" : (int)(mySlider.value * 100) + "%"; 
    }

    void OnSliderValueChanged(float value, TMP_Text myText, string parameterName, Slider mySlider)
    {
        value = (value == 0.0f) ? -80.0f : 20.0f * Mathf.Log10(value);

        myText.text = (value == -80.0f) ? "0%" : (int)(mySlider.value * 100) + "%";

        mixer.SetFloat(parameterName, value);
    }
}
