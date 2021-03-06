﻿using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class wappy_bird : MonoBehaviour
{

	public float tapForce = 10;
	public float tiltSmooth = 5;
	public Vector3 startPos;

	public AudioSource jump;
	public AudioSource die;
	public AudioSource coin;
	

	private new Rigidbody2D rigidbody;
	private Quaternion forwardRotation;
	private Quaternion downRotation;

	public delegate void PlayerDelegate();
	// event will be fired when player died and scored 
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;
	

	// Use this for initialization
	private void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		downRotation = Quaternion.Euler (0, 0, -75);
		forwardRotation = Quaternion.Euler (0, 0, 45);
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (!rigidbody.simulated)
			return;
		if (Input.GetKey(KeyCode.Space) || Input.touchCount == 1 && Input.GetTouch(0).phase != TouchPhase.Stationary) {
			jump.Play();
			rigidbody.velocity = Vector2.zero;
			transform.rotation = forwardRotation;
			rigidbody.AddForce (Vector2.up * tapForce, ForceMode2D.Force);
		} 
		transform.rotation = Quaternion.Lerp (transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
	}

	private void OnEnable()
	{
		GameManager.onGameStarted += onGameStarted;
		GameManager.onGameOverConfirmed += onGameOverConfirmed;
	}

	private void OnDisable()
	{
		GameManager.onGameStarted -= onGameStarted;
		GameManager.onGameOverConfirmed -= onGameOverConfirmed;
	}
	
	private void onGameStarted()
	{
		rigidbody.velocity = Vector2.zero;
		rigidbody.simulated = true;
	}

	private void onGameOverConfirmed()
	{
		transform.localPosition = startPos;
		transform.rotation = Quaternion.identity;
	}
	

	private void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag("DeadZone")) {
			rigidbody.simulated = false;
			// register dead event
			if (OnPlayerDied != null) OnPlayerDied();
			// play sound
			die.Play();
			
		}
		
		GetComponent<CircleCollider2D>().isTrigger = !col.CompareTag("Top");
		
		if (!col.gameObject.CompareTag("ScoreZone")) return;
		// register score event
		if (OnPlayerScored != null) OnPlayerScored(); // event sent to GameManager
		// play a sound
		coin.Play();
	}
}
