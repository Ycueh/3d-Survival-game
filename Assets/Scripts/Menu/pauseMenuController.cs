using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class pauseMenuController : MonoBehaviour
{    //Load file sign
    public GameObject loadDataObject;
    //Volume
    [Header("Volume")]
    [SerializeField] private Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    //[SerializeField] private GameObject comfirmationPrompt = null;
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private Toggle muteToggle;
    public AudioSource mute1;
    private GameObject[] invincibles;
    private GameObject[] speeds;
    private GameObject[] bonuses;
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

    [Header("Show")]
    public bool gamePaused = false;
    public GameObject pauseMenu;
    /*public Text timer;
    public float time, startTime;*/
    // Start is called before the first frame update
    void Start()
    {
        loadDataObject = GameObject.Find("LoadData");
        mute1 = GameObject.Find("BGMObj").GetComponent<AudioSource>();
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

        //Acquire the bases and bonuses
        invincibles = GameObject.FindGameObjectsWithTag("Invincible");
        speeds = GameObject.FindGameObjectsWithTag("Speed");
        bonuses = GameObject.FindGameObjectsWithTag("Bonus");
    }

    // Update is called once per frame
    void Update()
    {
        //updateTime();
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gamePaused)
            {
                Resume();
            }
            else { 
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
    }
    public void Pause() { 
        Debug.Log("Pause");
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        gamePaused = true;
    }

    public void Quit() {
        Application.Quit();
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Load();
            Time.timeScale = 1.0f;
            gamePaused = false;
        }
        else{
            noSaveDiaglog.SetActive(true);
        }
    }

    public void Exit(){
        Application.Quit();
    }
    
    public void SetVolume(float volume){
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
        mute1.mute = mute;
        foreach(GameObject invincible in invincibles)
       {
            invincible.GetComponent<AudioSource>().mute = mute;
       }
        foreach(GameObject speed in speeds){
            speed.GetComponent<AudioSource>().mute = mute;
        }

        foreach(GameObject bonus in bonuses){
            bonus.GetComponent<AudioSource>().mute = mute;
        }

        //StartCoroutine(ConfirmationBox());
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
        
         mute1.mute = mute;
        foreach(GameObject invincible in invincibles)
       {
            invincible.GetComponent<AudioSource>().mute = mute;
       }
        foreach(GameObject speed in speeds){
            speed.GetComponent<AudioSource>().mute = mute;
        }

        foreach(GameObject bonus in bonuses){
            bonus.GetComponent<AudioSource>().mute = mute;
        }
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
