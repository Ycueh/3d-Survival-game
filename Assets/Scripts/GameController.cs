using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    public int score=0;
    public float scoreTimer = 0.0f;
    public float doubleTimer=0.0f;
    public int scoreIncrement = 1;
    public float abilityTimer = 10.0f;
    public float abilityTimer1 = 10.0f;
    public Player player;
    public GameObject losWindow1;
    public Text text1;
    public GameObject losWindow2;
    public Text text2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {   
        doubleTimer+=Time.deltaTime;
        scoreTimer += Time.deltaTime;
        
        //Double the scoreincrement
        if(doubleTimer >= 60.0f){
            scoreIncrement = scoreIncrement*2;  
            doubleTimer = 0.0f;
        }
        if(scoreTimer >= 10.0f){
            score += scoreIncrement;
            scoreTimer = 0.0f;
        }
        
        if(score<10){
        Score.text = "Score:0" + score;
        }
        else{
            Score.text = "Score:"+score;
        }

        //Monitor the HP and Score
        if(player.HP<=0){
            Time.timeScale = 0.0f;
            if(score<50){
                losWindow1.SetActive(true);
                text1.text = "You get "+score+" this time";
            }else if(score>50){
                losWindow2.SetActive(true);
                text2.text = "You get "+score+" this time";
            }
        }
    }
}
