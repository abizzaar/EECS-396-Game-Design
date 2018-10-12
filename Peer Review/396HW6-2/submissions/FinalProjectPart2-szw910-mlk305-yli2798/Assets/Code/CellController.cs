using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Assets.Code.Menus;
using UnityEngine;

public class CellController : MonoBehaviour
{
	public static UIManager UI { get; private set; }

	private void Start()
	{
		UI = new UIManager();
	}

	void OnMouseDown()
	{
		if (Base.end)
		{
			return;
		}
		
		if (CompareTag("Clicked"))
		{
			gameObject.GetComponent<Renderer>().material.color = Color.gray;
			tag = "Cube";
			GameObject[] cubes = GameObject.FindGameObjectsWithTag("Disabled");
			foreach (var cub in cubes)
			{
				cub.tag = "Cube";
			}
			if (gameObject.layer == 9)
			{
				UI.HideUpgradeMenu();
				
			}
			else if (gameObject.layer == 8)
			{
				UI.HideBuildMenu();
			}	
		}
	    else if (!CompareTag("EnemyBase") && !CompareTag("HomeBase") && !CompareTag("Disabled") ){
		    gameObject.GetComponent<Renderer>().material.color = Color.red;
		    tag = "Clicked";
		    GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
		    foreach (var cub in cubes)
		    {
			    cub.tag = "Disabled";
		    }
			if (gameObject.layer == 8)
			{
				UI.ShowBuildMenu();
			}
			else if (gameObject.layer == 9)
			{
				UI.ShowUpgradeMenu();
			}
		}		
	}
}