using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollision : MonoBehaviour
{   
    public GameObject MagicFire;
    public bool touched;
	List<GameObject> objects = new List<GameObject>();
	private GameObject Environment;
	public GameController GameController;
	int score;

	public AudioClip collisionSound;
    // Use this for initialization
	void Start () {
		GameController = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>();
		Environment = GameObject.Find("Environment");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if(!touched){
				AudioSource.PlayClipAtPoint(collisionSound, transform.position);
			}
			//Record the collision
            touched = true;
			GameObject.Find("Environment").GetComponent<createElements>().objects.Add(this.gameObject);
			//Improve the score
			GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>().score += 5;
		}
	}
    
}
