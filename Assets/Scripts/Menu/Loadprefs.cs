using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loadprefs : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Setting")]
    [SerializeField] private Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;
    public AudioSource mute1;
    [SerializeField] private Toggle muteToggle;

    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private Text brightnessValue = null;

    [Header("Quality level setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("FullScreen")]
    [SerializeField] private Toggle fullScreenToggle;

    

    private void Awake(){
        mute1 = GameObject.Find("BGMObj").GetComponent<AudioSource>();
        if(canUse){
            //Settings Load
            if(PlayerPrefs.HasKey("Volume")){
                float localVolume = PlayerPrefs.GetFloat("Volume");
                Debug.Log(localVolume);
                volumeTextValue.text = (Mathf.Round(localVolume*100)).ToString();
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }else{
                AudioListener.volume = defaultVolume;
                volumeSlider.value = defaultVolume;
                volumeTextValue.text = (Mathf.Round(defaultVolume*100)).ToString();
            }

            if(PlayerPrefs.HasKey("quality")){
                int localQuality = PlayerPrefs.GetInt("quality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }
            if(PlayerPrefs.HasKey("isFullScreen")){
                int localFullScreen = PlayerPrefs.GetInt("isFullScreen");
                if(localFullScreen == 1){
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = true;
                }else{
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }
            if(PlayerPrefs.HasKey("Brightness")){
                float localBrightness = PlayerPrefs.GetFloat("Brightness");
                brightnessValue.text= localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
            }
            if(PlayerPrefs.HasKey("isMute")){
                 int localMute = PlayerPrefs.GetInt("isMute");

                if(localMute == 1){
                    mute1.mute = true;
                    muteToggle.isOn = true;
                }else{
                    mute1.mute = false;
                    muteToggle.isOn = false;
                }
            }


        }

    }



    
}
