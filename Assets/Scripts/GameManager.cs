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
			player.speed = 0.06f * score + 5.0f;
			player.spinSpeed = 0.06f * score + 5.0f;
		}
	}

	public void OnUserClick(){
		if (isGameStarted) {
			player.SwitchDirection ();
		} else {
			player.speed = 5f;
			isGameStarted = true;
			foreach (Text t in texts)
				t.enabled = false;
			foreach (Button b in buttons) {
				b.enabled = false;
				b.image.enabled = false;
			}
			scoreText.enabled = true;
		}
	}
}
