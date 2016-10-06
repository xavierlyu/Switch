using UnityEngine;
using System.Collections;
using System;

public class Base : MonoBehaviour {

	static bool flag;//true = top; false = bottom
	public static event Action switchDirectionEvent;
	public static GameManager gameManager;

	void Start(){
		gameManager = FindObjectOfType<GameManager> ();
		flag = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (switchDirectionEvent != null)
				switchDirectionEvent ();
			if((this.gameObject.tag.Equals("BaseTop") && flag) || (this.gameObject.tag.Equals("BaseBottom") && !flag)){
				gameManager.Scored ();
				flag = !flag;
			}
		}
	}
}
