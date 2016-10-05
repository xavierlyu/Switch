using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour {

	public float speed = 7; //goes up first
	public float spinSpeed = 7;
	public GameObject sprite;
	public Obstacle top;
	public Obstacle bottom;

	void Start(){
		Obstacle.switchDirectionEvent += SwitchDirection;
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