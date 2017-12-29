using System.Collections;
using System.Collections.Generic;
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
		var count = 3;
		for (var i = 0; i < count; i++)
		{
			countdown.text = count.ToString();
			count--;
			Debug.Log(count);
			yield return new WaitForSeconds(1);
		}
		onCountDownFinished();
	}
	
}
