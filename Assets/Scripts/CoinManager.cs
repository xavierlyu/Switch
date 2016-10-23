using UnityEngine;
using System.Collections;
using System;

public class CoinManager : Obstacle {

	bool audio = false;
	float timer;
	public static event Action OnCoinHit;//called when the player is hit by an obstacle

	// Use this for initialization
	void Start () {
		timer = 0.3f;

		Destroy (this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (audio) {
			timer -= Time.deltaTime;
			if(timer<0.01f)
				Destroy (this.gameObject);
		}
		transform.Translate (Vector2.right * speed * Time.deltaTime, Space.World);
	}

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt("Coins") + 1);
		audio = true;
		GetComponent<AudioSource> ().Play ();
		if (OnCoinHit != null)
			OnCoinHit ();
	}
}
