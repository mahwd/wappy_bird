using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class countDown : MonoBehaviour
{
	public Text countdown;

	public delegate void CountDownFinish();
	public static event CountDownFinish onCountDownFinished;
	
	private void OnEnable ()
	{
//		countdown = GetComponent<Text> ();
		countdown.text = "3";
		StartCoroutine ("CountDown"); 
	}

	private IEnumerator CountDown()
	{
		const int count = 3;
		for (var i = 0; i < count; i++)
		{
			countdown.text = (count-i).ToString();
			yield return new WaitForSeconds(1);
		}
		if (onCountDownFinished != null) onCountDownFinished();
	}
	
}
