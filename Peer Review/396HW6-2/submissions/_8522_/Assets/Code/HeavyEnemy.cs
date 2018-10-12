using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HeavyEnemy : Enemy {

	// Use this for initialization
	void Start () {
		
		homeBase = GameObject.FindObjectOfType<Base>();
		healthBar = transform.Find("healthBar");
		GetComponent<Renderer>().material.color = new Color(0.1f, 0.5f, 1.0f);
		healthBar.GetComponent<Renderer>().material.color = new Color(0.1f, 0.5f, 1.0f);
		life = 20;
		speed = .25f;
		damage = 20;
	
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

