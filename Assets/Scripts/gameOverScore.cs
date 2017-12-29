using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class gameOverScore : MonoBehaviour
{

	private Text scoreText;

	
	private void Start()
	{
		scoreText = GetComponent<Text>();
		scoreText.text = GameManager.Instance.score.ToString();		
	}

	private void FixedUpdate()
	{
		scoreText = GetComponent<Text>();
		scoreText.text = GameManager.Instance.score.ToString();
		
	}
}
