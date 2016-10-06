using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour {

	public float speed; //goes up first
	public float spinSpeed;
	public GameObject sprite;
	public Base top;
	public Base bottom;

	public GameManager gameManager;

	void Start(){
		gameManager = FindObjectOfType<GameManager> ();
		Base.switchDirectionEvent += SwitchDirection;
	}

	void Update () {
		if (Input.GetMouseButtonUp (0))
			SwitchDirection ();
		if (transform.position.y < bottom.transform.position.y) {
			speed = Mathf.Abs (speed);
			spinSpeed = Mathf.Abs (spinSpeed);
		}
		else if (transform.position.y > top.transform.position.y) {
			speed = Mathf.Abs (speed) * (-1);
			spinSpeed = Mathf.Abs (spinSpeed) * (-1);
		}
		transform.Translate (Vector2.up * speed * Time.deltaTime);
		sprite.transform.Rotate (Vector3.forward * spinSpeed);
	}

	public void SwitchDirection(){
		speed *= -1;
		spinSpeed *= -1;
	}
}