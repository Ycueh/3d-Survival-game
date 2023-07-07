using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public Player player;
    public GameObject InvincibleEffect;
    public GameObject SpeedEffect;
    public GameController gameController;
    public float walkSpeed = 2;
    public bool walking =false;
    public bool running = false;
    public string ability = null;
    private float speed;
    private float SP;
    Animator anim;
    Vector3 move;
    private bool play1 = false;
    private bool play2 = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other) {
       if(other.tag == "Speed"){
           GameObject fire = other.transform.GetChild(5).gameObject;
           if(fire.activeSelf == true){
           //timer = 10.0f;
           //walkSpeed = 4;
           //Disable the fire sign
           fire.SetActive(false);
           //No SP consuming
           player.ability = "Speed";
        }
       }else if(other.tag == "Invincible"){
           GameObject fire1 = other.transform.GetChild(5).gameObject;
           if(fire1.activeSelf == true){
           //timer = 10.0f;
           //Disable the fire sign
           fire1.SetActive(false);
           //No SP consuming
           player.ability = "Invincible";
           }
       }
    }

    // Update is called once per frame
    void Update()
    {   
        if(player.ability == "Speed"){
            //Activate the player particle effect
            if(!play1){
            SpeedEffect.SetActive(true);
            ParticleSystem sys = SpeedEffect.GetComponent<ParticleSystem>();
            sys.Play(true);
            play1 = true;
            }
            if(gameController.abilityTimer>0){
                walkSpeed = 4;
                gameController.abilityTimer -= Time.deltaTime;
            }else{
                walkSpeed = 2;
                SpeedEffect.SetActive(false);
                gameController.abilityTimer = 10.0f;
                player.ability = null;
                play1 = false;
            }
        }
        else if(player.ability == "Invincible"){
            //Activate the player particle effect
            if(!play2){
            InvincibleEffect.SetActive(true);
            ParticleSystem sys = InvincibleEffect.GetComponent<ParticleSystem>();
            sys.Play(true);
            play2 = true;
            }
            if(gameController.abilityTimer1>0){
                gameController.abilityTimer1 -= Time.deltaTime;
            }else{
                SpeedEffect.SetActive(false);
                gameController.abilityTimer1 = 10.0f;
                player.ability = null;
                InvincibleEffect.SetActive(false);
                play2 = false;
            }
        }
        SP = player.SP;
        /*if(timer>0){
            timer -=Time.deltaTime;
        }else{
            if(player.ability == "Speed"){
            walkSpeed = 2;
            SpeedEffect.SetActive(false);
            }
            else if(player.ability == "Invincible"){
            InvincibleEffect.SetActive(false);
            }
            player.ability = null;
        }
        SP = player.SP;*/

        //Move Controller
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        move = new Vector3(x,0,z);
        transform.LookAt(transform.position + new Vector3(x,0,z));
        if(Mathf.Abs(x)<0.5f&&Mathf.Abs(z)<0.5f){
            anim.SetBool("isWalking",false);
            walking = false;
        }else{
            anim.SetBool("isWalking",true);
            walking = true;
            speed = walkSpeed;
        }
        if ((Input.GetKey("left shift") || Input.GetKey("right shift"))){
            if(SP>10){
            anim.SetBool("isRunning",true);
            speed = walkSpeed*2;
            running = true;
            walking = false;
            }else{
                anim.SetBool("isRunning",false);
                running = false;
            }
        }else{
            anim.SetBool("isRunning",false);
            speed = walkSpeed;
            running = false;
        }
        transform.position += new Vector3(x,0,z)*speed *Time.deltaTime;
    }
}
