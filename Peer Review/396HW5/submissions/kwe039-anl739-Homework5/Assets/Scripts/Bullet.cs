using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float timeLeft;

	public void init(float lifeTime)
	{
		timeLeft = lifeTime;
	}	
	// Update is called once per frame
	void Update ()
	{
		if (timeLeft <= 0f) Destroy(gameObject);
		else timeLeft -= Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Destroy(gameObject);
	}
}
