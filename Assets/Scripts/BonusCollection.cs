using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BonusCollection : MonoBehaviour {


	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public GameController GameController;

	public AudioClip collectSound;

	int score;

	float radian = 0;
    public float perRadian = 0.03f;
	public float radius = 0.08f;

	private int localMute;

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find("GameController").GetComponent<GameController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 initialPos = transform.position;
        radian += perRadian;
        float y_deviation = Mathf.Cos(radian) * radius;
        transform.position = initialPos + new Vector3(0, y_deviation * 0.1f, 0);
		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
			//Generate new bonus
		}
	}

	public void Collect()
	{	
		if(PlayerPrefs.HasKey("isMute")){
            localMute = PlayerPrefs.GetInt("isMute");
        }
		if(collectSound){
			if(localMute == 0){
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
			}
		}
		//Below is space to add in your code for what happens based on the collectible type
		GameController.score += 10;
		Destroy (gameObject);
	}
}
