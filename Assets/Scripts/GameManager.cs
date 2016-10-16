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
	float speedConstant;
	float spawnConstant;
	void Start () {
		gameStatus = GameStatus.BeforeStart;
		player = FindObjectOfType<PlayerManager> (); 
		score = 0;
		accumulator = timeToSpawn;
		speedConstant = 1;
		spawnConstant = 1;
	}
	
	void Update () {
		
		
		if (gameStatus == GameStatus.InGame) {
			speedConstant += Time.deltaTime / 100;
			spawnConstant -= Time.deltaTime / 500;

			if (speedConstant > 2)
				speedConstant = 2;
			if (spawnConstant < 0.5f)
				spawnConstant = 0.5f;
			
			accumulator -= Time.deltaTime;
			if (accumulator <= 0f) {
				float randomPosition = Mathf.Ceil(Random.Range (-2.7f, 3f));
				float randomSize = Random.Range (0.5f, 0.75f);
				GameObject temp;
				if (Random.value > 0.5f) {
					temp = Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector2 (-5f, randomPosition), Quaternion.identity) as GameObject;
					temp.GetComponent<Obstacle> ().speed = Random.Range (0.75f, 2f)*speedConstant;
				} else {
					temp = Instantiate (obstacles [Random.Range (0, obstacles.Length)], new Vector2 (5f, randomPosition), Quaternion.identity) as GameObject;
					temp.GetComponent<Obstacle> ().speed = Random.Range (-0.75f, -2f)*speedConstant;
				}
				temp.transform.localScale = new Vector3 (randomSize, randomSize, 1f);
				accumulator = timeToSpawn * spawnConstant;
			}
		}
	}

	public void Scored(){
		if (gameStatus == GameStatus.InGame) {
			score++;
			scoreText.text = score + "";
			player.speed += Mathf.Sign(player.speed) * 0.08f;
			player.spinSpeed = player.speed;
			timeToSpawn -= 0.02f;
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
			timeToSpawn = 1f;
			scoreText.text = 0+"";
			Base.flag = true;
		}
		else if(gameStatus == GameStatus.AfterEnd){
			if (gameEndAnimators[0].GetCurrentAnimatorStateInfo (0).IsName("ScoreTextIn") && 
				gameEndAnimators[0].GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !gameEndAnimators[0].IsInTransition (0)) {//if the animation is finished
				foreach(Animator a in gameStartAnimators){
					a.SetBool ("Flag", false);
				}
				foreach (Animator a in gameEndAnimators) {
					a.SetBool ("Flag", false);
				}
				gameStatus = GameStatus.BeforeStart;
				scoreText.enabled = false;
				player.gameObject.transform.position = new Vector3 (0f,0f,-2f);
				playerAnimator.SetBool ("Flag", false);
			}
		}
	}
}
