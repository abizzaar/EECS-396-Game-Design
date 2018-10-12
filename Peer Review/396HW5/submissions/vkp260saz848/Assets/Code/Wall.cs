using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private int screen_height;
    private int screen_length;

	// Use this for initialization
	void Start () {
        FitToScreen();
	}
	
    void FitToScreen()
    {
        screen_height = Screen.height;
        screen_length = Screen.width;
        if (name == "Wall1")
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, screen_height / 2, 0));
            transform.position = worldPoint;
            transform.localScale = new Vector3(0.1f, screen_height, 0);
        }
        else if (name == "Wall2")
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screen_length / 2, screen_height, 0));
            transform.position = worldPoint;
            transform.localScale = new Vector3(screen_length, 0.1f, 0);
        }
        else if (name == "Wall3")
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screen_length, screen_height / 2, 0));
            transform.position = worldPoint;
            transform.localScale = new Vector3(0.1f, screen_height, 0);
        }
        else
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screen_length / 2, 0, 0));
            transform.position = worldPoint;
            transform.localScale = new Vector3(screen_length, 0.1f, 0);
        }

    }

	// Update is called once per frame
	void Update () {
        if(screen_height != Screen.height || screen_length != Screen.width)
        {
            FitToScreen();
        }
	}
}
