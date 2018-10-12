using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    private GameObject healthBar;

	// Use this for initialization
	void Start () {
        healthBar = GameObject.Find("Canvas").transform.Find("healthBarSlider").gameObject;
        GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
		if (healthBar.GetComponent<Slider>().value <= 0)
		{
			Globals.pm.forDaLose();
		}
	}

    public void ReceiveDamage(int dmg)
    {
        healthBar.GetComponent<Slider>().value -= dmg;
    }
}
