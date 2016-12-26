using UnityEngine;
using System.Collections;
using System;

public class Base : MonoBehaviour {

	public static bool flag;//true = top; false = bottom
	public static event Action OnPlayerSwitchDirection;
	public static GameManager gameManager;
	private Animator anim;
	private AudioSource audioSource;

	private static Sprite base_notLit;//aka you
	public Sprite base_lit;

	void Start(){
		base_notLit = gameObject.GetComponent<SpriteRenderer> ().sprite;
		if (gameObject.tag.Equals ("BaseTop")) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = base_lit;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = base_notLit;
		}
		audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		gameManager = FindObjectOfType<GameManager> ();
		flag = true;
	}

	void Update(){
		if (tag.Equals ("BaseTop")) {
			GetComponent<SpriteRenderer> ().sprite = flag ? base_lit : base_notLit;
		} else {
			GetComponent<SpriteRenderer> ().sprite = flag ? base_notLit : base_lit;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(GameManager.isAudioOn)
				audioSource.Play (); // play bounce sound
			//StartCoroutine ("PlayOneShot");
			if (OnPlayerSwitchDirection != null)
				OnPlayerSwitchDirection ();
			if((gameObject.tag.Equals("BaseTop") && flag) || (gameObject.tag.Equals("BaseBottom") && !flag)){
				gameManager.Scored ();
				flag = !flag;
			}
		}
	}

	IEnumerator PlayOneShot(){
		anim.SetTrigger("Collided");
		yield return new WaitForSeconds(0.2f);
		anim.ResetTrigger ("Collided");
	}
}
