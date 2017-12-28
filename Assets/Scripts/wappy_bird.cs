using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class wappy_bird : MonoBehaviour
{

	public float tapForce = 10;
	public float tiltSmooth = 5;
	public Vector3 startPos;

	Rigidbody2D rigidbody;
	Quaternion forwardRotation;
	Quaternion downRotation;


	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		downRotation = Quaternion.Euler (0, 0, -75);
		forwardRotation = Quaternion.Euler (0, 0, 45);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			transform.rotation = forwardRotation;
			rigidbody.velocity = Vector2.zero;
			rigidbody.AddForce (Vector2.up * tapForce, ForceMode2D.Force);
		} 
		transform.rotation = Quaternion.Lerp (transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		Debug.Log ("trigger");
		if (col.gameObject.tag == "DeadZone") {
			Debug.Log ("DeadZone");
			rigidbody.simulated = false;
			// register dead event 
			// play sound
		}
		if (col.gameObject.tag == "ScoreZone") {
			Debug.Log ("ScoreZone");
			// register score event
			// play a sound
		}
	}
}
