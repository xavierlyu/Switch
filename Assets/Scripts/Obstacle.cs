using UnityEngine;
using System.Collections;
using System;

public class Obstacle : MonoBehaviour {

	public float speed;

	public static event Action OnPlayerHit;//called when the player is hit by an obstacle

	void Start(){
		Destroy (this.gameObject, 5f);
		if (gameObject.name.Equals ("kunai(Clone)") && transform.position.x < 0)
			transform.Rotate (0f,0f,-180f);
	}

	void Update(){
		transform.Translate (Vector2.right * speed * Time.deltaTime, Space.World);
		if(!gameObject.name.Equals("kunai(Clone)"))
			transform.Rotate (Vector3.forward * -speed, Space.Self);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (OnPlayerHit != null)
				OnPlayerHit ();
		}
	}
}