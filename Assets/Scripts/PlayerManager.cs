using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public static float speed = 7; //goes up first
	public static float spinSpeed = 7;
	public GameObject sprite;
	public Obstacle top;
	public Obstacle bottom;
	
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

	public static void SwitchDirection(){
		speed *= -1;
		spinSpeed *= -1;
	}
}