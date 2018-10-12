using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Update ()
	{
		Vector2 size = Vector2.one;
		size.x = Camera.main.orthographicSize * Screen.width / Screen.height;
		size.y = Camera.main.orthographicSize;
		transform.localScale = new Vector3(size.x, size.y, 1f);
	}
}
