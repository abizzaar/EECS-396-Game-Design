using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {

    int width;
    int height;

    // Use this for initialization
    void Start () {
        Resize();
    }

    void Resize() { // Resize the wall
        width = Screen.width;
        height = Screen.height;
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        Vector3 rightUp = camera.ScreenToWorldPoint( new Vector3(width, height, 0));
        Vector3 leftUp = camera.ScreenToWorldPoint(new Vector3(0, height, 0));
        Vector3 leftDown = camera.ScreenToWorldPoint(new Vector3(0, 0,0));
        Vector3 rightDown = camera.ScreenToWorldPoint(new Vector3(width, 0,0));

        Vector2[] newPoints = new Vector2[5];

        newPoints[0] = new Vector2(rightUp.x,rightUp.y);
        newPoints[1] = new Vector2(leftUp.x,leftUp.y);
        newPoints[2] = new Vector2(leftDown.x, leftDown.y);
        newPoints[3] = new Vector2(rightDown.x,rightDown.y);
        newPoints[4] = new Vector2(rightUp.x, rightUp.y);

        GetComponent<EdgeCollider2D>().points = newPoints;
    }
	
	// Update is called once per frame
	void Update () {
		if(Screen.width!= width || Screen.height != height) {
            Resize();
        }
	}
}
