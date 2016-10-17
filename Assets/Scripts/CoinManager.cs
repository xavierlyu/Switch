using UnityEngine;
using System.Collections;
using System;

public class CoinManager : Obstacle {

	public static event Action OnCoinHit;//called when the player is hit by an obstacle

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * speed * Time.deltaTime, Space.World);
	}

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt("Coins") + 1);
		Destroy (this.gameObject);
		if (OnCoinHit != null)
			OnCoinHit ();
	}
}
