using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class highscore : MonoBehaviour
{
	private Text highscoreText;

	private void Start()
	{
		highscoreText = GetComponent<Text>();
		highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
	}

	private void FixedUpdate()
	{
		highscoreText = GetComponent<Text>();
		highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
	}
}
