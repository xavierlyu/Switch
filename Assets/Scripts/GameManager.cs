using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject obj;
	public Transform[] spawnPoints;
	float accumulator;
	public float timeToSpawn;
	public int score;

	public Text scoreText;

	void Start () {
		score = 0;
		accumulator = timeToSpawn;
	}
	
	void Update () {
		accumulator -= Time.deltaTime;
		if (accumulator <= 0f) {
			int randomPositionIndex = Random.Range (0, spawnPoints.Length);
			if (randomPositionIndex <= 3)
				(Instantiate (obj, spawnPoints [randomPositionIndex].position, Quaternion.identity) as GameObject).GetComponent<Obstacle> ().speed = Random.Range (1.5f,5f);
			else if(randomPositionIndex >= 4)
				(Instantiate (obj, spawnPoints [randomPositionIndex].position, Quaternion.identity) as GameObject).GetComponent<Obstacle> ().speed = Random.Range (-1.5f,-5f);
			accumulator = timeToSpawn;
		}
	}

	public void Scored(){
		score++;
		scoreText.text = score + "";
	}
}
