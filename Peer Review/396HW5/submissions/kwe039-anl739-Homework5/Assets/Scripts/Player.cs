using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{

	private Rigidbody2D rb;
	private PolygonCollider2D coll;
	public Transform spawn;
	public bulletManager bm;
	public scoreManager sm;
	public bool player;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player)
		{
			Turn(Input.GetAxis("P1Horizontal"));
			Thrust(Input.GetAxis("P1Vertical"));

			if (Input.GetAxis("P1" + platform.fireAxis) > 0f)
			{
				bm.Fire(spawn);
			}
		}
		else
		{
			Turn(Input.GetAxis("P2Horizontal"));
			Thrust(Input.GetAxis("P2Vertical"));

			if (Input.GetAxis("P2" + platform.fireAxis) > 0f)
			{
				bm.Fire(spawn);
			}
		}
	}
	
	/* CONTROLS */
	
	private void Turn (float direction) {
		if (Mathf.Abs(direction) < 0.02f) { return; }
		rb.AddTorque(direction * -0.1f);
	}

	private void Thrust (float intensity) {
		if (Mathf.Abs(intensity) < 0.02f) { return; }
		rb.AddRelativeForce(Vector2.up * intensity);
	}
	
	/* COLLISIONS + SCORING */
	
	internal void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.GetComponent<Bullet>())
		{
			if (!other.transform.IsChildOf(bm.transform))
			{
				sm.AddScore(2, !player);
			}
			else
			{
				sm.AddScore(-1, player);
			}
		}
	}
	
}
