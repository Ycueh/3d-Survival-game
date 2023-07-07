using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    bool touched;
    bool detected;
    Animator anim;
    public UnityEngine.AI.NavMeshAgent navAgent;
    int score;
    public GameObject GameController;
    public GameObject enemy;
    public GameObject environment;
    private GameObject touchObject;
    public createElements createController;

    private float initialSpeed = 0.2f;
    private float timer = 0.0f;

    void Start(){
        anim = GetComponent<Animator>();
        navAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        createController = GameObject.FindGameObjectWithTag("Environment").GetComponent<createElements>(); 
        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = initialSpeed;

    }
    void Update(){
       followFunction();
       if(timer>2.0f){
           if(initialSpeed<1.0f){
           initialSpeed +=0.1f;
           timer = 0.0f;
           }
       }else{
           timer +=Time.deltaTime;
       }
       //Velocity increment
       this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = initialSpeed;
    }

    void OnTriggerEnter(Collider other){
       touchObject = other.gameObject;
      /* Debug.Log(other.tag);
       Debug.Log("Bug area"+createController.objects);*/
       //Remove the base
       if(createController.objects.Contains(touchObject)){
       createController.objects.Remove(touchObject);
       Destroy(touchObject);
       //Create new Creature
       GameObject newEnemy =  Instantiate(enemy,other.transform.position,Quaternion.identity);
       newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (float)navAgent.speed/2;
       //Reduce the speed
       this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (float)navAgent.speed;
       }
       
    }

    void followFunction(){
        //If there exists touched base
        if(createController.objects.Count>0){
            detected = true;
            int index = createController.objects.Count-1;
            if(createController.objects[index]){
            Transform targetBase = createController.objects[index].transform;
            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination=targetBase.position;
            }else{
                createController.objects.Remove(createController.objects[index]);
            }
        }
        else if(target!=null){
            detected = true;
            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination=target.transform.position;
        }
        anim.SetBool("detected",detected);
    }
}
