using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
	private static Object _bullet;
	private const float SpawnTime = 1.2f;
	private float LastSpawn;
	

	// Use this for initialization
	void Start()
	{
		_bullet = Resources.Load("Bullet");
				
	}

	// Update is called once per frame
	void Update ()
	{
		if (Base.end)
		{
			return;
		}
		if ((Time.time - LastSpawn) < SpawnTime) return;
		LastSpawn = Time.time;
		ForceSpawn();
	}

	public static void ForceSpawn()
	{
		
		//var tower = GameObject.FindGameObjectWithTag("Clicked");
		towerBuild[] towers = FindObjectsOfType<towerBuild>();
		foreach (towerBuild tower in towers)
		{
			if (tower.nearest != null && tower.nearest.gameObject.activeSelf)
			{
				Vector3 bulletPos = new Vector3(tower.transform.position.x,tower.transform.position.y + 2,tower.transform.position.z);
				GameObject newBullet = (GameObject)Instantiate(_bullet,bulletPos,Quaternion.identity);
				Bullet newb = newBullet.GetComponent<Bullet>();
				newb.near = tower.nearest;
			}
		
		}
		
	}
}


