using UnityEngine;
using System.Collections;
using System;

public class CoinManager : Obstacle {

	public static event Action OnCoinHit;//called when the player is hit by an obstacle
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0.75f, 0.75f, 0.75f); //prevents random coin size, I think it looks better not changing
		audioSource = GetComponent<AudioSource> ();
		Destroy (this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * speed * Time.deltaTime, Space.World);
	}

	protected override void OnTriggerEnter (Collider other)
	{
		PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt("Coins") + 1);
		GetComponent<Animator> ().Play ("coinCollected");
		if (GameManager.isAudioOn) 
			audioSource.Play ();
		if (OnCoinHit != null)
			OnCoinHit ();
		Destroy (this.gameObject, 0.5f);
	}
}
