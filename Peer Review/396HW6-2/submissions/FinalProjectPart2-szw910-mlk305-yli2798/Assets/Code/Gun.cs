using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

	private const float FireCooldown = 1f;
	private float _lastfire;
	

	public void Fire()
	{
		float time = Time.time;
		if (time < _lastfire + FireCooldown)
		{
			return;
		}

		_lastfire = time;

		BulletManager.ForceSpawn();

	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

