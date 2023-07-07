using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{   
    //Save Enemy info
    public Vector3 basePosition;
    public double baseSpeed;
    public List<double> enemySpeeds = new List<double>();
    public List<Vector3> enemyPositions = new List<Vector3>();
    //Save Bonus info
    public List<Vector3> bonusPositions = new List<Vector3>();
    //Save Base info
    public List<Vector3> basePositions = new List<Vector3>();
    public List<string> typeLists = new List<string>();
    public List<bool> touched = new List<bool>();


}
