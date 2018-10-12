using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletManager : MonoBehaviour {

	// Use this for initialization
	public GameObject bullet;

	public float velocity;
	public float bulletLifespan;
	public float fireRate = 1f;
	private float timer = 0f;

	void Update()
	{
		timer += Time.deltaTime;
	}

	public void Fire(Transform spawnPoint)
	{
		if (timer >= fireRate)
		{
			Bullet tempBullet = ((GameObject) Instantiate(bullet, spawnPoint)).GetComponent<Bullet>();
			tempBullet.GetComponent<Rigidbody2D>().velocity = spawnPoint.up.normalized * velocity;
			tempBullet.init(bulletLifespan);
			tempBullet.transform.SetParent(transform);
			timer = 0f;
		}
	}
}
