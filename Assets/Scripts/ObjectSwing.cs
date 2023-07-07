using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwing : MonoBehaviour
{
    float radian = 0;
    public float perRadian = 0.03f;
    public float radius = 0.08f;
    public float rotationSpeed = 90;
    Vector3 initialPos;
    void Start(){
        initialPos = transform.position;
    }
    void Update(){
        initialPos = transform.position;
        radian+=perRadian;
        float y_deviation = Mathf.Cos(radian)*radius;
        transform.position = initialPos+new Vector3(0,y_deviation,0);

        //Rotate
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
