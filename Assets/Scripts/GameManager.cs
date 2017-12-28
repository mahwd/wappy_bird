using UnityEngine;
using UnityEngine.UI;

public class GameManager:MonoBehaviour
{
	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countDownPage;
	public Text scoreText;
	// ReSharper disable once MemberCanBePrivate.Global
	public static GameManager Instance;

	public delegate void GameDellegate ();

#pragma warning disable 67
	public static event GameDellegate onGameStarted;
#pragma warning restore 67
	public static event GameDellegate onGameOverConfirmed;

	private enum pageState
	{
		None,
		startPage,
		gameOverPage,
		countDownPage,
	}

	private int score = 0;
	private bool gameOver = false;

	public bool GameOver{ get { return gameOver; } }

	// ReSharper disable once ArrangeTypeMemberModifiers
	void Awake ()
	{
		Instance = this;
	}

	
	private void setPageState (pageState state)
	{
		switch (state) {
		case pageState.None:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (false);
			break;
		case pageState.startPage:
			startPage.SetActive (true);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (false);
			break;
		case pageState.gameOverPage:
			startPage.SetActive (false);
			gameOverPage.SetActive (true);
			countDownPage.SetActive (false);
			break;
		case pageState.countDownPage:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countDownPage.SetActive (true);
			break;

		default:
			break;
		}
	}

	public void ConfirmGameOver ()
	{
		// will be activated when replay button 
		// ReSharper disable once PossibleNullReferenceException
		onGameOverConfirmed ();
		scoreText.text = "0";
		setPageState (pageState.startPage);
	}

	// ReSharper disable once UnusedMember.Local
	public void StartGame ()
	{
		// will be activated when start game button hitted
		setPageState (pageState.countDownPage);
	}
}


