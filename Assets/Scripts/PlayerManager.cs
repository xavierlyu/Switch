using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public static float speed = 7;
	public static float spinSpeed = 7;
	public GameObject sprite;
	
	void Update () {
		if (Input.GetMouseButtonUp (0))
			SwitchDirection ();
		transform.Translate (Vector2.up * speed * Time.deltaTime);
		sprite.transform.Rotate (Vector3.forward * spinSpeed);
	}

	public static void SwitchDirection(){
		speed *= -1;
		spinSpeed *= -1;
	}
}