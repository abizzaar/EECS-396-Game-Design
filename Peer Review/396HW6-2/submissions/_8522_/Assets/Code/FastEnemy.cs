using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FastEnemy : Enemy {

	// Use this for initialization
	void Start () {
		
		homeBase = GameObject.FindObjectOfType<Base>();
		healthBar = transform.Find("healthBar");
		GetComponent<Renderer>().material.color = new Color(0.3f, 0.9f, 0.5f);
		healthBar.GetComponent<Renderer>().material.color = new Color(0.3f, 0.9f, 0.5f);
		life = 6;
		speed = 1f;
		damage = 5;
	
		NavMeshAgent nma = GetComponent<NavMeshAgent>();
		nma.destination = homeBase.transform.position;
		//nma.Warp(homeBase.transform.position);
		nma.speed = speed;
	}

	void Update()
	{
		showHealthBar();
	}
}

