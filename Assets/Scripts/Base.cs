using UnityEngine;
using System.Collections;
using System;

public class Base : MonoBehaviour {

	public static bool flag;//true = top; false = bottom
	public static event Action OnPlayerSwitchDirection;
	public static GameManager gameManager;
	public Sprite notBent;
	public Sprite bent;
	private SpriteRenderer sr;

	void Start(){
		gameManager = FindObjectOfType<GameManager> ();
		flag = true;
		sr = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D other){
		sr.sprite = bent;
		if (other.tag == "Player") {
			if (OnPlayerSwitchDirection != null)
				OnPlayerSwitchDirection ();
			if((this.gameObject.tag.Equals("BaseTop") && flag) || (this.gameObject.tag.Equals("BaseBottom") && !flag)){
				gameManager.Scored ();
				flag = !flag;
			}
		}
	}

	void OnTriggerExit2D()
	{
		sr.sprite = notBent;
	}
}
