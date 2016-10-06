using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public float speed;

	void Start(){
		Destroy (this.gameObject, 5f);
	}

	void Update(){
		transform.Translate (Vector2.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.LogError ("Hit!");
		}
	}
}