using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update(){
        if(GameObject.FindGameObjectsWithTag("BGM")!=null){
           GameObject[] objects = GameObject.FindGameObjectsWithTag("BGM");
           if(objects.Length>1){
           for(int i =1;i<objects.Length;i++){
               Destroy(objects[i]);
           }
        }
       }
    }

}

