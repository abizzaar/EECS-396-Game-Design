using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
	private GameObject Normal;
	private GameObject Quick;
	private GameObject Strong;
	private GameObject Spawner;
	private Vector3 initialNormalPos;
	private Vector3 initialStrongPos;
	private Vector3 initialQuickPos;
	private bool waveNumChanged;
	
	// Use this for initialization
	void Start()
	{
		Normal = GameObject.Find("Normal");
		Quick = GameObject.Find("Quick");
		Strong = GameObject.Find("Strong");
		Spawner = GameObject.Find("Spawner");
		initialNormalPos = Normal.GetComponent<RectTransform>().localPosition;
		initialStrongPos = Strong.GetComponent<RectTransform>().localPosition;
		initialQuickPos = Quick.GetComponent<RectTransform>().localPosition;
		waveNumChanged = Spawner.GetComponent<Spawner>().waveNumChanged;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float waveNumber = Spawner.GetComponent<Spawner>().waveNumber;
		float waveWaitTime = Spawner.GetComponent<Spawner>().waveWaitTime;
		int enemiesNormal = Spawner.GetComponent<Spawner>().numberOfEnemiesNormal;
		int enemiesStrong = Spawner.GetComponent<Spawner>().numberOfEnemiesStrong;
		int enemiesQuick = Spawner.GetComponent<Spawner>().numberOfEnemiesFast;
		

		if (waveNumber > 1)
		{
			float inc = Time.deltaTime * 1000 / waveWaitTime;
			
			Normal.GetComponent<RectTransform>().localPosition = new Vector3
			(
				Normal.GetComponent<RectTransform>().localPosition.x + inc,
				Normal.GetComponent<RectTransform>().localPosition.y,
				Normal.GetComponent<RectTransform>().localPosition.z
			);

				Normal.GetComponent<RectTransform>().localPosition = initialNormalPos;
			
		}
		

	}
}
