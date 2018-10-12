using System.Collections;
using System.Collections.Generic;
using Assets.Code.Menus;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Grid : MonoBehaviour {

	public GameObject plane;
	public int width = 5;
	public int height = 5;

	private GameObject[,] grid = new GameObject[5,5];
	private int label = 1;
	
	
	void Start()
	{

		for (int x = 0; x < width; x++)
		{
			for (int z = 0; z < height; z++)
			{
				GameObject gridPlane = (GameObject) Instantiate(plane);
				gridPlane.GetComponent<Renderer> ().material.color = Color.gray;
				if (label == 5)
				{
					gridPlane.tag = "EnemyBase";
				}
				if (label == 21)
				{
					gridPlane.tag = "HomeBase";
				}
				label++;
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x*5, 
					gridPlane.transform.position.y, gridPlane.transform.position.z + z*5 );
				grid[x, z] = gridPlane;
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
