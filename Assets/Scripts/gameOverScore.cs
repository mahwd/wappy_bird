using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All
[RequireComponent(typeof(Text))]
public class gameOverScore : MonoBehaviour
{
	// Text
	private Text scoreText;

	private GameManager _game;
	
	private void Start()
	{
		_game = GameManager.Instance;
		scoreText = GetComponent<Text>();
		scoreText.text = GameManager.Instance.score.getFullAmount().ToString();		
	}

	private void FixedUpdate()
	{
		if (!_game.GameOver) return;
		scoreText = GetComponent<Text>();
		scoreText.text = _game.score.manat + " manat\n" + _game.score.qepik + " qepik";
	}
}
