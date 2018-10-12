using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rb;

	
	internal void Start () {
		_rb = GetComponent<Rigidbody2D>();
	}
	
	internal void Update () {
		HandleInput();
	}

	/// <summary>
	/// Check the controller for player inputs and respond accordingly.
	/// </summary>
	private void HandleInput () {
		if (Input.GetAxis ("Horizontal") != 0) {
			Turn (Input.GetAxis ("Horizontal"));
		}
		if (Input.GetAxis ("Vertical") != 0) {
			Thrust (Input.GetAxis ("Vertical"));
		
		}
	}

	private void Turn (float direction) {
		if (Mathf.Abs(direction) < 0.02f) { return; }
		_rb.AddTorque(direction * -0.05f);
	}

	private void Thrust (float intensity) {
		if (Mathf.Abs(intensity) < 0.02f) { return; }
		_rb.AddRelativeForce(Vector2.up * -intensity);
	}


}
