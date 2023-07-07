using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPController : MonoBehaviour
{   
    private bool noConsuming;
    public Player player;
    [SerializeField] private float maxSP=100.0f;
    [SerializeField] private bool running;
    [HideInInspector] public bool hasGenerated = true;
    [SerializeField] private Image SPProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;
    void Start(){
        player = GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {   
        running = GetComponent<MoveController>().running;
        string ability = player.ability;
        if(ability == "Speed"){
            noConsuming = true;
        }else{
            noConsuming = false;
        }
        //If human is walking
        if(!running){
            if(player.SP<=maxSP-0.01){
                Invoke("recoverSP",1.0f);
                if(player.SP>maxSP){
                hasGenerated = true;
                }
                SPProgressUI.fillAmount = player.SP/maxSP;
            }
        }else{
            if(player.SP>0){
                if(!noConsuming){
                Invoke("reduceSP",1.0f);}
                if(player.SP>maxSP){
                hasGenerated = true;
                }
                SPProgressUI.fillAmount = player.SP/maxSP;
            }
        }
    }

    void reduceSP(){
        player.SP -= 10.0f*Time.deltaTime;
    }
    void recoverSP(){
        player.SP += 20.0f*Time.deltaTime;
    }

}
