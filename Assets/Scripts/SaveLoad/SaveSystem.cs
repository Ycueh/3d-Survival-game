using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(){
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path)){
 
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;;
            stream.Close();

            return data;
        }else{
            Debug.LogError("Save file not found in "+ path);
            return null;
        }
    }

    public static void SaveEnemy(GameObject[] enemyObjects,GameObject baseEnemy, GameObject[] bonusList, GameObject[] baseList){
        SaveData enemies = new SaveData();
        //Save Enemy info
        enemies.baseSpeed = baseEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        enemies.basePosition = baseEnemy.transform.position;
        foreach(GameObject enemy in enemyObjects){
            if(enemy != baseEnemy){
            enemies.enemySpeeds.Add(enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed);
            enemies.enemyPositions.Add(enemy.transform.position);
            }
        }
        //Save Bonus and Base
        foreach(GameObject bonus in bonusList){
            enemies.bonusPositions.Add(bonus.transform.position);
        }

       foreach(GameObject baseObject in baseList){
          enemies.basePositions.Add(baseObject.transform.position);
          enemies.typeLists.Add(baseObject.tag);
          enemies.touched.Add(baseObject.GetComponent<BaseCollision>().touched);
       }

        var serializer = new XmlSerializer(typeof(SaveData));
        string path = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        serializer.Serialize(stream,enemies);
        stream.Close();

        Debug.Log("Saved");

    }

    public static SaveData LoadEnemy(){
        string path = Application.persistentDataPath + "/data.save";
        if(File.Exists(path)){
           var serializer = new XmlSerializer(typeof(SaveData));
           FileStream stream = new FileStream(path, FileMode.Open);
           SaveData data = serializer.Deserialize(stream) as SaveData;
           stream.Close();
           return data;

        }else{
            Debug.LogError("Save file not found in "+ path);
            return null;
        }
    }

   
}
