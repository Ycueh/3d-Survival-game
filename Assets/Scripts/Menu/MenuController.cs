using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuController : MonoBehaviour
{   
    //Load file sign
    public GameObject loadDataObject;
    //Volume
    [Header("VolumeControl")]
    [SerializeField] private Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    //[SerializeField] private GameObject comfirmationPrompt = null;
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private Toggle muteToggle;
    public AudioSource audioSource;
    private bool mute;

    public int gameLevel;
    private string levelToload;
    [SerializeField] private GameObject noSaveDiaglog = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private Text brightnessValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int qualityLevel;
    private bool fullScreen;
    private float brightnessLevel;
   

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    

    private void Start(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
         
        int currentResolutionIndex = 0;

        for(int i = 0;i<resolutions.Length;i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width &&resolutions[i].height == Screen.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void SetResoultion(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }


    public void newGameDialogYes(){
        SceneManager.LoadScene(gameLevel);
    }

    public void loadGameDialogYes()
    {
        string path = Application.persistentDataPath + "/data.save";
        string path1 = Application.persistentDataPath + "/player.save";
        if(File.Exists(path)&&File.Exists(path1)){
            //Record and transport data
            loadDataObject.GetComponent<LoadData>().Loaded = true;
            SceneManager.LoadScene(1);
        }
        else{
            noSaveDiaglog.SetActive(true);
        }
        PlayerPrefs.SetInt("loaded",(loadDataObject.GetComponent<LoadData>().Loaded?1:0));
    }

    public void Exit(){
        Application.Quit();
    }
    
    public void SetVolume(float volume){
        Debug.Log(volume);
        AudioListener.volume = volume;
        volumeTextValue.text = (Mathf.Round(volume*100)).ToString();
    }

    public void VolumeApply(){
        //Volume
        PlayerPrefs.SetFloat("Volume",AudioListener.volume);
        

        //Save Graphics
        PlayerPrefs.SetFloat("Brightness",brightnessLevel);

        PlayerPrefs.SetInt("quality",qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("isFullScreen",(fullScreen?1:0));
        Screen.fullScreen = fullScreen;

        PlayerPrefs.SetInt("isMute",(mute?1:0));
        audioSource.mute = mute;

    }

    public void ResetButton(){
        AudioListener.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
        volumeTextValue.text = (Mathf.Round(defaultVolume*100)).ToString();

        brightnessSlider.value = defaultBrightness;
        brightnessValue.text = defaultBrightness.ToString("0.0");
        qualityDropdown.value = 1;
        QualitySettings.SetQualityLevel(1);
        fullScreenToggle.isOn = false;
        Screen.fullScreen = false;

        Resolution currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width,currentResolution.height,Screen.fullScreen);
        resolutionDropdown.value = resolutions.Length;

        
        muteToggle.isOn = false;
        Screen.fullScreen = false;
        audioSource.mute = false;
        VolumeApply();

        

    }

    public void SetBrightness(float brightness){
        brightnessLevel = brightness;
        brightnessValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen){
        fullScreen = isFullScreen;
    }

    public void SetMute(bool isMute){
        mute = isMute;
    }

    public void SetQuality(int index){
        qualityLevel = index;
    }

    

    
}
