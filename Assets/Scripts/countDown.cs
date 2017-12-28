using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(Text))]
public class countDown : MonoBehaviour
{
	private Text countdown;

	public delegate void CountDownFinish();
	public static event CountDownFinish onCountDownFinished;
	private void onEnable ()
	{
		countdown = GetComponent<Text> ();
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
			yield return new WaitForSeconds(1);
		}
		onCountDownFinished();
	}
	
}
