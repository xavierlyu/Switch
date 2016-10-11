using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] obstacles;
	float accumulator;
	public float timeToSpawn;
	public int score;
	private bool isGameStarted;
	public static bool isHit;

	public PlayerManager player;

	public Text scoreText;
	public Text[] texts;//texts wanted to be disabled when game starts
	public Button[] buttons;//buttons wanted to be disabled when game starts

	public Animator[] gameStartAnimators;

	void Start () {
		player = FindObjectOfType<PlayerManager> (); 
		isGameStarted = false;
		isHit = false;
		score = 0;
		accumulator = timeToSpawn;
	}
	
	void Update () {
		if (isGameStarted) {
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
	}

	public void Scored(){
		if (!isHit) {
			score++;
			scoreText.text = score + "";
			player.speed += 0.05f;
			player.spinSpeed += 0.05f;
			timeToSpawn -= 0.015f;
		}
	}

	public void OnPlayerDeath(){
		isHit = true;
		isGameStarted = false;
		Obstacle.OnPlayerHit -= OnPlayerDeath;
	}

	public void OnUserClick(){
		if (isGameStarted) {
			player.SwitchDirection ();
		} else {
			//game init goes here
			foreach(Animator a in gameStartAnimators){
				a.SetTrigger ("GameStart");
			}
			Obstacle.OnPlayerHit += OnPlayerDeath;
			score = 0;
			player.speed = 5f;
			isHit = false;
			isGameStarted = true;
			scoreText.enabled = true;
			timeToSpawn = 1.3f;
			scoreText.text = 0+"";
			Base.flag = true;
		}
	}
}
