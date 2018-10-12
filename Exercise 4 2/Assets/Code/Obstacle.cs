using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private bool movingUp = true;
	public float speed;
	
	// Update is called once per frame
	void Update ()
	{
		if (movingUp)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
			if (transform.position.y >= 26) movingUp = false;
		}
		else
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
			if (transform.position.y <= 7) movingUp = true;
		}
	}
}
