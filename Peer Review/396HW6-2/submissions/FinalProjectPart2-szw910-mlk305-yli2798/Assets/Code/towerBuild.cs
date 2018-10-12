using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBuild : MonoBehaviour {

	public Enemy nearest;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		if (Base.end)
		{
			return;
		}
		
		Enemy[] enemys = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		//Enemy nearest = null;
		var shortest = float.MaxValue;
		foreach (Enemy enemy in enemys)
		{
			if (((enemy.transform.position - transform.position).sqrMagnitude) < shortest)
			{
				shortest = (enemy.transform.position - transform.position).sqrMagnitude;
				nearest = enemy;
			}
		}

		if (shortest < float.MaxValue)
		{
			Vector3 relativepos = nearest.transform.position -transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativepos);
			transform.rotation = rotation;
		}
	}
}
