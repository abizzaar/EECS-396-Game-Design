using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private bool _flag = false;
	private const float _lifeTime = 3f;
	private float _deathTime;

	/// <summary>
	/// Bullet Speed
	/// </summary>
	private static float _speed = 25f;

	void Update()
	{
		if (Time.time >= _deathTime)
		{
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (_flag) Destroy(this.gameObject);
	}
	
	/// <summary>
	/// Initialize projectile
	/// </summary>
	public void Initialize(GameObject bullet, Vector3 direction)
	{
		GetComponent<Rigidbody>().velocity = _speed * direction;
		_deathTime = Time.time + _lifeTime;
		_flag = true;
	}
}



