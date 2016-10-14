using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour {

	public float speed; //goes up first
	public float spinSpeed;
	public Base top;
	public Base bottom;

	public GameManager gameManager;

	void Start(){
		spinSpeed = 5f;
		gameManager = FindObjectOfType<GameManager> ();
		Base.OnPlayerSwitchDirection += SwitchDirection;
		Obstacle.OnPlayerHit += OnPlayerDeath;
	}

	void Update () {
		if(GameManager.gameStatus == GameStatus.AfterEnd){
			speed = Mathf.Lerp (speed, 0f, Time.deltaTime * 2f);
			spinSpeed = Mathf.Lerp (spinSpeed, 0f, Time.deltaTime * 2f);
			if (Mathf.Abs(speed) < 0.05f && Mathf.Abs(spinSpeed) < 0.05f) {
				speed = 0f;
				spinSpeed = 0f;
			}
		}
		else if(GameManager.gameStatus == GameStatus.BeforeStart){
			speed = 0f;
			spinSpeed = 5f;
		}
		if (transform.position.y < bottom.transform.position.y) {
			speed = Mathf.Abs (speed);
			spinSpeed = Mathf.Abs (spinSpeed);
		}
		else if (transform.position.y > top.transform.position.y) {
			speed = Mathf.Abs (speed) * (-1);
			spinSpeed = Mathf.Abs (spinSpeed) * (-1);
		}
		transform.Translate (Vector2.up * speed * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.forward * spinSpeed, Space.Self);
	}
		
	public void SwitchDirection(){
		speed *= -1;
		spinSpeed *= -1;
	}

	public void OnPlayerDeath(){
	}
}