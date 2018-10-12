using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

public class Base : MonoBehaviour {

	private int _health;    
    private HealthBarManager healthBar;

	void Start ()
	{
		_health = 100;
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBarManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	internal void OnCollisionEnter(Collision other)
	{
		DealDamage(other.gameObject);
	}

	private void DealDamage(GameObject enemy)
	{
		if (enemy.tag == "normalEnemy")
		{
            if (_health < 5)
                _health = 0;
            else
                _health -= 5;

            healthBar.setHealth(_health);
		}
		if (enemy.tag == "fastEnemy")
		{
			if (_health < 1)
                _health = 0;
            else
                _health -= 1;

            healthBar.setHealth(_health);
        }
		if (enemy.tag == "slowEnemy")
		{
			if (_health < 10)
                _health = 0;
            else
                _health -= 10;

            healthBar.setHealth(_health);
        }
	}
}
