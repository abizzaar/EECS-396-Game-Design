using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	private const float SpawnTime = 0.8f;
	//private const float WaveTime = 10f;
	private float LastSpawn;
	private static Object _enemyPrefab;
	private int i = 0;
	public GameObject NumWaveText;
	public static int WaveNumber;
	//public static int 
	

	private float TargetTime = 15.0f;
	// Use this for initialization
	void Start ()
	{
		_enemyPrefab = Resources.Load("Enemy");
		WaveNumber = 1;
		NumWaveText = GameObject.FindGameObjectWithTag("NumWave");
		//NumWaveText.GetComponent<Text>().text = "NumWave: " + WaveNumber;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Base.end)
		{
			return;
		}
		
		if (WaveNumber > 5)
		{
			return;
		}
		TargetTime -= Time.deltaTime;
		if (TargetTime <= 0.0f)
		{
			if (Time.time - LastSpawn > 10f)
			{
				TargetTime = 15f;
				WaveNumber++;
				//Debug.Log(Time.time);
				Debug.Log(WaveNumber);
				//NumWaveText.GetComponent<Text>().text = "NumWave: " + WaveNumber;
				i = 0;
			}

		}
		else
		{
			if ((Time.time - LastSpawn) < SpawnTime) return;

			if (i <= 17)
			{
				LastSpawn = Time.time;
				Spawn(i);
				i++;
				//Debug.Log(i);
			}
			


		}

	}

	private void Spawn(int i)
	{
		var go = (GameObject) Instantiate(_enemyPrefab);
		if (i % 3 == 0)
		{
			go.GetComponent<Renderer> ().material.color = Color.blue;
			Enemy newE = go.GetComponent<Enemy>();
			newE.enemyHealth = 100;

		}
		
		//fast
		else if (i % 3 == 1)
		{
			go.GetComponent<Renderer> ().material.color = Color.cyan;
			Enemy newE = go.GetComponent<Enemy>();
			newE.enemyHealth = 100;
		}
		
		else if (i % 3 == 2)
		{
			go.GetComponent<Renderer> ().material.color = Color.green;
			Enemy newE = go.GetComponent<Enemy>();
			newE.enemyHealth = 200;
		}
	}
	
	

	
}
