using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public GameObject TOWER;
	private GameObject _tower;
	public static Enemy nearest;

	// Use this for initialization
	void Start()
	{
		_tower = (GameObject) Instantiate(TOWER);
		_tower.GetComponent<Renderer>().material.color = Color.yellow;


	}

	// Update is called once per frame
	void Update()
	{
		Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		//Enemy nearest = null;
		var shortest = float.MaxValue;
		foreach (Enemy enemy in enemies)
		{
			if (((enemy.transform.position - _tower.transform.position).sqrMagnitude) < shortest)
			{
				shortest = (enemy.transform.position - _tower.transform.position).sqrMagnitude;
				nearest = enemy;
			}
		}

		if (shortest < float.MaxValue)
		{
			Vector3 relativepos = nearest.transform.position - _tower.transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativepos);
			_tower.transform.rotation = rotation;
		}






	}
}

