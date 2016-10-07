using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] obstacles;
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
			float randomPosition = Random.Range (-3f, 3f);
			if(Random.value > 0.5f)
				(Instantiate(obstacles[Random.Range(0,obstacles.Length)], new Vector2(-5f,randomPosition), Quaternion.identity) as GameObject).GetComponent<Obstacle> ().speed = Random.Range (1.5f,5f);
			else
				(Instantiate(obstacles[Random.Range(0,obstacles.Length)], new Vector2(5f,randomPosition), Quaternion.identity) as GameObject).GetComponent<Obstacle> ().speed = Random.Range (-1.5f,-5f);
			accumulator = timeToSpawn;
		}
	}

	public void Scored(){
		score++;
		scoreText.text = score + "";
	}
}
