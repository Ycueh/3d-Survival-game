using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float HP;
    public float SP;
    public string ability;
    public float[] position;
    public int score;
    public float scoreTimer;
    public float doubleTimer;
    public float abilityTimer;
    public float abilityTimer1;

    public PlayerData(Player player){
        HP = player.HP;
        SP = player.SP;
        ability = player.ability;
        score = player.score;
        scoreTimer = player.scoreTimer;
        abilityTimer = player.abilityTimer;
        doubleTimer = player.doubleTimer;
        abilityTimer1 = player.abilityTimer1;

        position = new float[3];
        position[0] =player.transform.position.x;
        position[1]=player.transform.position.y;
        position[2]=player.transform.position.z;
    }
}
