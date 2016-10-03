using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			PlayerManager.SwitchDirection ();
		}
	}
}