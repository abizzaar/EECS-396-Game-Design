using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Code.Structure;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int enemyhealth;
    
    public int DamageValue;
    
    public float speed = .7f;
    private GameObject pbase;
    private Rigidbody body;
    

    // Use this for initialization
    void Start ()
    {
        pbase = GameObject.Find("Base");
        body = gameObject.GetComponent<Rigidbody>();
        //By putting these in the start function, 
        //later we can assign values by the enemy's tag or something
        enemyhealth = 3; 
	    DamageValue = -5;
	}
	
	// Update is called once per frame
	void Update () {
		Expire();
        Move();
	}

    void Expire()
    {
        if (enemyhealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject.Destroy(gameObject);
    }

    void Move()
    {
       
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pbase.transform.position, step);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            enemyhealth = enemyhealth - other.gameObject.GetComponent<Bullet>().damage;
        }

        if (other.gameObject.GetComponent<Base>() != null)
        {
            Die();
        }
    }
}
