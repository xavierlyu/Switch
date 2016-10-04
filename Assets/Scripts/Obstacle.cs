using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

//	public Sprite collided;
//	public SpriteRenderer sr;
//	public Sprite normal;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {	
			//sr.sprite = collided;
			PlayerManager.SwitchDirection ();
		}
	}

//	void OnTriggerExit2D(Collider2D other)
//	{
//		if (other.tag == "Player") {	
//			sr.sprite = normal;
//		}
//	}
}