using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All

public class GameManager:MonoBehaviour
{
	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countDownPage;
	public GameObject inGamePage;
	public GameObject pausePage;
	public Text scoreText;
	public static GameManager Instance;
	
	public delegate void GameDellegate ();
	// event for starting and restarting game 
	public static event GameDellegate onGameStarted;
	public static event GameDellegate onGameOverConfirmed;

	
	private enum pageState
	{
		None,
		inGamePage,
		pausePage,
		startPage,
		gameOverPage,
		countDownPage,
	}

	public int score = 0;
	private bool gameOver = true;

	public bool GameOver{ get { return gameOver; } }

	void Awake ()
	{
		Instance = this;
		setPageState((pageState.startPage));
	}

	private void OnEnable()
	{
		countDown.onCountDownFinished += onCountDownFinished;
		wappy_bird.OnPlayerScored += OnPlayerScored;
		wappy_bird.OnPlayerDied += OnPlayerDied;
	}

	private void OnDisable()
	{
		countDown.onCountDownFinished -= onCountDownFinished;
		wappy_bird.OnPlayerScored -= OnPlayerScored;		
		wappy_bird.OnPlayerDied -= OnPlayerDied;
	}

	private void onCountDownFinished () {
		setPageState(pageState.inGamePage);
		if (onGameStarted != null) onGameStarted ();
		score = 0;
		gameOver = false;
	}

	private void OnPlayerScored()
	{
		score++;
		scoreText.text = score.ToString();
	}

	private void OnPlayerDied()
	{
		gameOver = true;
		var highscore = PlayerPrefs.GetInt("HighScore");
		Debug.Log(score);
		Debug.Log(highscore);
		if (score >= highscore)
		{
			PlayerPrefs.SetInt("HighScore", score);
		}
		setPageState(pageState.gameOverPage);
	}

	private void setPageState (pageState state)
	{
		switch (state) {
		case pageState.None:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (false);
			inGamePage.SetActive (false);
			pausePage.SetActive (false);
			break;
		case pageState.startPage:
			startPage.SetActive (true);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (false);
			inGamePage.SetActive (false);
			pausePage.SetActive (false);
			break;
		case pageState.gameOverPage:
			startPage.SetActive (false);
			gameOverPage.SetActive (true);
			countDownPage.SetActive (false);
			inGamePage.SetActive (false);
			pausePage.SetActive (false);
			break;
		case pageState.countDownPage:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (true);
			inGamePage.SetActive (false);
			pausePage.SetActive (false);
			break;
		case pageState.inGamePage:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (false);
			inGamePage.SetActive (true);
			pausePage.SetActive (false);
			break;
		case pageState.pausePage:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (false);
			inGamePage.SetActive (false);
			pausePage.SetActive (true);
			break;
			default:
			break;
		}
	}

	
	// ui actions
	public void ConfirmGameOver ()
	{
		// will be activated when replay button 
		onGameOverConfirmed ();
		scoreText.text = "0";
		setPageState (pageState.startPage);
	}

	public void StartGame ()
	{
		// will be activated when start game button hitted
		setPageState (pageState.countDownPage);	
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
		setPageState(pageState.pausePage);
	}
	
	public void ResumeGame()
	{
		Time.timeScale = 1;
		setPageState(pageState.inGamePage);
	}
}


