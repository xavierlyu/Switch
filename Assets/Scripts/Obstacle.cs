using UnityEngine;
using System.Collections;
using System;

public class Obstacle : MonoBehaviour {

//	public Sprite collided;
//	public SpriteRenderer sr;
//	public Sprite normal;

	public static event Action switchDirectionEvent;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (switchDirectionEvent != null)
				switchDirectionEvent ();
		}
	}

//	void OnTriggerExit2D(Collider2D other)
//	{
//		if (other.tag == "Player") {	
//			sr.sprite = normal;
//		}
//	}
}