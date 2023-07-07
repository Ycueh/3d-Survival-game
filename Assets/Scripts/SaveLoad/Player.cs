using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{   
    public float HP;
    public float SP;
    public string ability;
    public Vector3 position;
    [Header("GameController")]
    public int score;
    public float scoreTimer;
    public float abilityTimer;
    public float abilityTimer1;
    public float doubleTimer;
    [Header("GameObject")]
    public GameObject baseEnemy;
    public GameObject bonus;
    public GameObject speedBase;
    public GameObject invincibleBase;
    // Start is called before the first frame update
    public void Save()
    {   
        //Save Player and Score
        score = GameObject.Find("GameController").GetComponent<GameController>().score;
        scoreTimer = GameObject.Find("GameController").GetComponent<GameController>().scoreTimer;
        abilityTimer = GameObject.Find("GameController").GetComponent<GameController>().abilityTimer;
        abilityTimer1 = GameObject.Find("GameController").GetComponent<GameController>().abilityTimer1;
        doubleTimer = GameObject.Find("GameController").GetComponent<GameController>().doubleTimer;
        SaveSystem.SavePlayer(this);
        //Save Enemy
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
       
        //Save Bonus and Base
        GameObject[] BonusObjects = GameObject.FindGameObjectsWithTag("Bonus");
        GameObject[] speedBases = GameObject.FindGameObjectsWithTag("Speed");
        //Merge two kinds of base
        GameObject[] invincibleBases = GameObject.FindGameObjectsWithTag("Invincible");
        GameObject[] baseLists = merge(speedBases,invincibleBases);
        SaveSystem.SaveEnemy(enemyObjects,baseEnemy,BonusObjects,baseLists);
    }

    // Update is called once per frame
    public void Load()
    {
        GameObject.Find("pauseMenuController").GetComponent<pauseMenuController>().Resume();
        //Load player info
        PlayerData data = SaveSystem.LoadPlayer();
        HP = data.HP;
        SP = data.SP;
        ability = data.ability;
        //Set GameController
        GameObject.Find("GameController").GetComponent<GameController>().score = data.score;
        GameObject.Find("GameController").GetComponent<GameController>().scoreTimer = data.scoreTimer;
        GameObject.Find("GameController").GetComponent<GameController>().abilityTimer = data.abilityTimer ;
        GameObject.Find("GameController").GetComponent<GameController>().abilityTimer1 = data.abilityTimer1;
        GameObject.Find("GameController").GetComponent<GameController>().doubleTimer = data.doubleTimer;
        int test = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>().score;


        //Transform the player position
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;


        //Load Enemy
        SaveData data1 = SaveSystem.LoadEnemy();
        Vector3 basePosition = data1.basePosition;
        double speed = data1.baseSpeed;
        List<double> enemySpeeds = data1.enemySpeeds;
        List<Vector3> enemyPositions = data1.enemyPositions;
        //Destroy all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies){
            if(enemy != baseEnemy){
            Destroy(enemy);}
        }
        //Load the base enemy
        baseEnemy.transform.position = basePosition;
        baseEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (float)speed;

        //Generate the new GameObject
        for(int i =0;i<enemySpeeds.Count;i++){
        GameObject newEnemy =  Instantiate(baseEnemy, enemyPositions[i],Quaternion.identity);
        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (float)enemySpeeds[i];
        }

        resetEnvironment();
        //Load Bonus, Base
        List<Vector3> bonusPositions = data1.bonusPositions;
        foreach(Vector3 bonusPosition in bonusPositions){
            Instantiate(bonus, bonusPosition,Quaternion.identity);
        }
        //Load Base Info
        List<Vector3> basePositions = data1.basePositions;
        List<string> typeLists = data1.typeLists;
        List<bool> touched =  data1.touched;
        GameObject loadBase = null;
        for(int i =0;i<typeLists.Count;i++){
            if(typeLists[i]=="Speed"){
                loadBase =  Instantiate(speedBase, basePositions[i],Quaternion.identity);
                
            }else if(typeLists[i]=="Invincible"){
                loadBase =  Instantiate(invincibleBase, basePositions[i],Quaternion.identity);
            }
            if(touched[i]==true){
                loadBase.transform.GetChild(5).gameObject.SetActive(false);
            }
        }
    }

    public static GameObject[] merge(GameObject[] first, GameObject[]second){
    GameObject[] result = new GameObject[first.Length + second.Length];
     Array.Copy(first, 0, result, 0, first.Length);
     Array.Copy(second, 0, result, first.Length, second.Length);
     return result;
    }

    private void resetEnvironment(){
        GameObject[] BonusObjects = GameObject.FindGameObjectsWithTag("Bonus");
        if(BonusObjects!=null){
            foreach(GameObject bonus in BonusObjects){
                Destroy(bonus);
            }
        }
        GameObject[] speedBases = GameObject.FindGameObjectsWithTag("Speed");
        //Merge two kinds of base
        GameObject[] invincibleBases = GameObject.FindGameObjectsWithTag("Invincible");
        GameObject[] baseLists = merge(speedBases,invincibleBases);
        if(baseLists != null){
            foreach(GameObject baseobj in baseLists){
                Destroy(baseobj);
            }
        }
    }
}
