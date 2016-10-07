using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public GameObject sprite;
	public float speed;

	void Start(){
		Destroy (this.gameObject, 5f);
	}

	void Update(){
		transform.Translate (Vector2.right * speed * Time.deltaTime);
		sprite.transform.Rotate (Vector3.forward * -speed);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.LogError ("Hit!");
		}
	}
}