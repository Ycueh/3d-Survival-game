using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createElements : MonoBehaviour
{
    // Start is called before the first frame update
    float timer1 = 0.0f;
    float timer2 = 0.0f;
    int loaded;
    public GameObject bonus;
    public GameObject baseObject1;
    public GameObject baseObject2;
    private GameObject baseObject;
    int[,] planeArray = new int[120,120];
    public int bonusNumber = 20;
    public int baseNumber = 40;
    private float height;
    public List<GameObject> objects = new List<GameObject>();
    private int leftBound = 42;
    private int rightBound = 160;

    void Start()
    {   
        GameObject.Find("pauseMenuController").GetComponent<pauseMenuController>().Resume();
        if(PlayerPrefs.HasKey("loaded")){
            loaded = PlayerPrefs.GetInt("loaded");
        }
        //If this is the loaded file, Load the saved data
        if(loaded==1){
            Debug.Log("Loaded");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Load();
            PlayerPrefs.SetInt("loaded",0);
        }
        else{
        Instantiate(baseObject1,new Vector3(50,0,95),Quaternion.identity);
        //Generate bonus item
        for(int i =1;i<=bonusNumber;i++){
            int xcount = Random.Range(leftBound,rightBound);
            int zcount = Random.Range(leftBound,rightBound);
            Vector3 pos = new Vector3(xcount,2,zcount);
            height = Terrain.activeTerrain.SampleHeight(pos);
            Instantiate(bonus,new Vector3(xcount,height+1,zcount),Quaternion.identity);

        }

        //Generate base item
        for(int j =1;j<=baseNumber;j++){
            int number = Random.Range(0,2);
            if(number<1){
                baseObject = baseObject1;
            }else{
                baseObject = baseObject2;
            }
            int xcount = Random.Range(leftBound,rightBound);
            int zcount = Random.Range(leftBound,rightBound);
            Vector3 pos1 = new Vector3(xcount,2,zcount);
            height = Terrain.activeTerrain.SampleHeight(pos1);
            Instantiate(baseObject,new Vector3(xcount,height,zcount),Quaternion.identity);

        }
    }
    }

    // Update is called once per frame
    void Update()
    {
        bonusRege();
        baseRege();
        //ReGenerate new bonus and base
    }

    void bonusRege(){
        GameObject[] bonusObjects = GameObject.FindGameObjectsWithTag("Bonus");
        if(bonusObjects.Length<10){
            timer1 += Time.deltaTime;
            if(timer1>=2.0f){
            int xcount = Random.Range(leftBound,rightBound);
            int zcount = Random.Range(leftBound,rightBound);
            Vector3 pos = new Vector3(xcount,2,zcount);
            height = Terrain.activeTerrain.SampleHeight(pos);
            Instantiate(bonus,new Vector3(xcount,height+2,zcount),Quaternion.identity);
           //Instantiate(bonus,new Vector3(50,0,34),Quaternion.identity);
            timer1 = 0.0f;
            }
        }
    }

    void baseRege(){
        GameObject[] InvincibleObjects = GameObject.FindGameObjectsWithTag("Invincible");
        GameObject[] speedObjects = GameObject.FindGameObjectsWithTag("Speed");
        
        if((InvincibleObjects.Length + speedObjects.Length)<21){
            timer2 += Time.deltaTime;
            if(timer2>=5.0f){
            int number = Random.Range(0,2);
            if(number<1){
                baseObject = baseObject1;
            }else{
                baseObject = baseObject2;
            }
            int xcount = Random.Range(leftBound,rightBound);
            int zcount = Random.Range(leftBound,rightBound);
            Vector3 pos1 = new Vector3(xcount,2,zcount);
            height = Terrain.activeTerrain.SampleHeight(pos1);
            Instantiate(baseObject,new Vector3(xcount,height,zcount),Quaternion.identity);
            //Instantiate(baseObject,new Vector3(50,0,34),Quaternion.identity);
            timer2 = 0.0f;
            }
        }
    }
}
