using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class highscore : MonoBehaviour
{
	private Text highscoreText;

	private void Start()
	{
		highscoreText = GetComponent<Text>();
		highscoreText.text = "HighScore: " + PlayerPrefs.GetString("PrintHighScore");
	}

	private void FixedUpdate()
	{
		if (!GameManager.Instance.GameOver) return;
		highscoreText = GetComponent<Text>();
		highscoreText.text = PlayerPrefs.GetString("PrintHighScore");
	}
}
