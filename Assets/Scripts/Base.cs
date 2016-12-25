using UnityEngine;
using System.Collections;
using System;

public class Base : MonoBehaviour {

	public static bool flag;//true = top; false = bottom
	public static event Action OnPlayerSwitchDirection;
	public static GameManager gameManager;
	private Animator anim;
	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		gameManager = FindObjectOfType<GameManager> ();
		flag = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(GameManager.isAudioOn)
				audioSource.Play (); // play bounce sound
			StartCoroutine ("PlayOneShot");
			if (OnPlayerSwitchDirection != null)
				OnPlayerSwitchDirection ();
			if((this.gameObject.tag.Equals("BaseTop") && flag) || (this.gameObject.tag.Equals("BaseBottom") && !flag)){
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
