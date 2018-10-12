using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class Base : MonoBehaviour {

	public GameObject Cylin;
	public Slider Meter;
	public static int hit;//value on slider 
	public GameObject Text;
	public static float dollar;//
	public static bool end;//indicate if the game has ended
	public static float towerprice;//price of building a tower
	
	void Start ()
	{
		end = false;
		hit = 100;
		towerprice = 100;
		GameObject _base = (GameObject) Instantiate(Cylin);

		_base.GetComponent<Renderer> ().material.color = Color.red;

		var go = GameObject.Find("Slider");
		Meter = go.GetComponent<Slider>();
		Meter.value = hit;
		//Debug.Log(Meter.value);

		Text = GameObject.FindGameObjectWithTag("Money");
		dollar = 150;
		Text.GetComponent<Text>().text = "$ " + dollar;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (end)
		{
			return;
		}
		//Debug.Log(hit);
		if (hit <= 0)
		{
			Instantiate(Resources.Load("Lose"),GameObject.Find("Canvas").transform);
			end = true;
			return;
		}
		Meter.value = hit;
		Text.GetComponent<Text>().text = "$ " + dollar;
		if (EnemyManager.WaveNumber > 5)
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (var enemy in enemies)
			{
				if (enemy.gameObject.activeSelf)
				{
					return;
				}
			}
			Instantiate(Resources.Load("Win"),GameObject.Find("Canvas").transform);
			end = true;
		}
	}


}

