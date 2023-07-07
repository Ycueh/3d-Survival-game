using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private float timer =2.0f;
    private bool inDanger = false;
    public Player player;
    public string ability;
    [SerializeField] private float maxHP=100.0f;
    [SerializeField] private Image HPProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;
     void Start(){
        player = GetComponent<Player>();
    }

    void Update()
    {   
        if(inDanger){
            timer-=Time.deltaTime;
            //Debug.Log(timer);
            if(timer<=0 && player.ability!="Invincible"){
                player.HP -=1.0f;
                timer = 2.0f;
            }
        }
        HPProgressUI.fillAmount = player.HP/maxHP;

        
    }
    void OnTriggerEnter(Collider other)
	{   
        if(other.tag == "DangerZone"){
            inDanger = true;
        }
		if (other.tag == "Enemy" && player.ability!="Invincible") {
			player.HP -= 10.0f;
		}
	}
    private void OnTriggerExit(Collider other) {
        if(other.tag == "DangerZone"){
            inDanger = false;
        }
        
    }

}
