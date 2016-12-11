using UnityEngine;
using System.Collections;
using System;

public class Obstacle : MonoBehaviour {

	public float speed;

	public static event Action OnPlayerHit;//called when the player is hit by an obstacle

	void Start(){
		Destroy (this.gameObject, 5f);
	}

	void Update(){
		transform.Translate (Vector2.right * speed * Time.deltaTime, Space.World);
		transform.Rotate (-speed/2.0f, -speed, -speed/2.0f, Space.Self);
	}

	protected virtual void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			if (OnPlayerHit != null)
				OnPlayerHit ();
		}
	}
}