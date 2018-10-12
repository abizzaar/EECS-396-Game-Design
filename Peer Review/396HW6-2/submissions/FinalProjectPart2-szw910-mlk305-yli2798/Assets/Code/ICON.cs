using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ICON : MonoBehaviour
{
	
	public Object icon;
	// Use this for initialization
	void Start () {
		icon = Resources.Load("ICON");
		for (int i = 1; i <= 5; i++)
		{
			GameObject newicon = (GameObject) Instantiate(icon,GameObject.Find("Canvas").transform);
			var texts = newicon.GetComponentsInChildren<Text>();
			foreach (var _text in texts)
			{
				if (_text.name == "NumWave")
				{
					_text.text = "Wave: " + i;
				}
			}
			Vector3 newpos=new Vector3(55+(i-1)*120, newicon.transform.position.y, newicon.transform.position.z);
			//Debug.Log(newpos);
			newicon.transform.position = newpos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Base.hit <= 0)
		{
			return;
		}
		
		//icon = GameObject.FindGameObjectWithTag("ICON");

		GameObject[] allicons = GameObject.FindGameObjectsWithTag("ICON");
		foreach (var icon in allicons)
		{
			icon.transform.position = icon.transform.position + 0.135f*Vector3.left;
			//Debug.Log(icon.transform.position.x);
			if (icon.transform.position.x < 50)
			{
				icon.gameObject.SetActive(false);
			} 
		}	
	}
}
