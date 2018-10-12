using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Walls : MonoBehaviour
{
	private float height;
	private EdgeCollider2D edgeCollider2D;
	private Camera mainCamera;

	void Start()
	{
		edgeCollider2D = GetComponent<EdgeCollider2D>();
		mainCamera = FindObjectOfType<Camera>();
	}
	void Update ()
	{
		// getting height and width of screen and updating size of edgeCollider using its "points" category
		Vector2 point1 = new Vector2(0, 0);
		point1 = mainCamera.ScreenToWorldPoint(point1);
		Vector2 point2 = new Vector2(Screen.width, 0);
		point2 = mainCamera.ScreenToWorldPoint(point2);
		Vector2 point3 = new Vector2(Screen.width, Screen.height);
		point3 = mainCamera.ScreenToWorldPoint(point3);
		Vector2 point4 = new Vector2(0, Screen.height);
		point4 = mainCamera.ScreenToWorldPoint(point4);
		Vector2 point5 = point1;
		point5 = mainCamera.ScreenToWorldPoint(point5);
		Vector2[] points = {point1, point2, point3, point4, point5};
		edgeCollider2D.points = points;
	}
}
