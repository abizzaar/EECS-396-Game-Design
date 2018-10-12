using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : Enemy {

    // Use this for initialization
    void Start () {
        homeBase = GameObject.FindObjectOfType<Base>();
        healthBar = transform.Find("healthBar");
        GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        healthBar.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        life = 10;
        speed = 0.5f;
        damage = 10;
        
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
