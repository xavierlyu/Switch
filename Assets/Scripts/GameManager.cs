using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameStatus
{
	BeforeStart,
	InGame,
	AfterEnd
};

public class GameManager : MonoBehaviour {
	
	public GameObject[] obstacles;
	float accumulator;
	public float timeToSpawn;
	public int score;
	public static GameStatus gameStatus;

	public PlayerManager player;

	public Text scoreText;
	public Text highScoreText;
	public Text[] texts;//texts wanted to be disabled when game starts
	public Button[] buttons;//buttons wanted to be disabled when game starts

	public Animator playerAnimator;
	public Animator[] gameStartAnimators;
	public Animator[] gameEndAnimators;

	void Start () {
		gameStatus = GameStatus.BeforeStart;
		player = FindObjectOfType<PlayerManager> (); 
		score = 0;
		accumulator = timeToSpawn;
	}
	
	void Update () {
		if (gameStatus == GameStatus.InGame) {
			accumulator -= Time.deltaTime;
			if (accumulator <= 0f) {
				float randomPosition = Random.Range (-3f, 3f);
				if (Random.value > 0.5f)
					(Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector2 (-5f, randomPosition), Quaternion.identity) as GameObject).GetComponent<Obstacle> ().speed = Random.Range (1.5f, 5f);
				else
					(Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector2 (5f, randomPosition), Quaternion.identity) as GameObject).GetComponent<Obstacle> ().speed = Random.Range (-1.5f, -5f);
				accumulator = timeToSpawn;
			}
		}
	}

	public void Scored(){
		if (gameStatus == GameStatus.InGame) {
			score++;
			scoreText.text = score + "";
			player.speed += 0.05f;
			player.spinSpeed += 0.05f;
			timeToSpawn -= 0.015f;
		}
	}

	public void OnPlayerDeath(){
		gameStatus = GameStatus.AfterEnd;
		if (score > PlayerPrefs.GetInt ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", score);
		highScoreText.text = "BEST " + PlayerPrefs.GetInt ("HighScore");
		playerAnimator.SetBool ("Flag", true);
		foreach (Animator a in gameEndAnimators) {
			a.SetBool ("Flag", true);
		}
		Obstacle.OnPlayerHit -= OnPlayerDeath;
	}

	public void OnUserClick(){
		if (gameStatus == GameStatus.InGame) {
			player.SwitchDirection ();
		} 
		else if (gameStatus == GameStatus.BeforeStart){
			//game init goes here
			foreach(Animator a in gameStartAnimators){
				a.SetBool ("Flag", true);
			}
			Obstacle.OnPlayerHit += OnPlayerDeath;
			score = 0;
			player.speed = 5f;
			gameStatus = GameStatus.InGame;
			scoreText.enabled = true;
			timeToSpawn = 1.3f;
			scoreText.text = 0+"";
			Base.flag = true;
		}
		else if(gameStatus == GameStatus.AfterEnd){
			foreach(Animator a in gameStartAnimators){
				a.SetBool ("Flag", false);
			}
			foreach (Animator a in gameEndAnimators) {
				a.SetBool ("Flag", false);
			}
			gameStatus = GameStatus.BeforeStart;
			scoreText.enabled = false;
			player.gameObject.transform.position = new Vector3 (0f,0f,0f);
			playerAnimator.SetBool ("Flag", false);
		}
	}
}
