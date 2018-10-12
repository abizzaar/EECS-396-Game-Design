using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{

	public int p1Score;
	public int p2Score;
	public Text p1ScoreText;
	public Text p2ScoreText;
	
	// Use this for initialization
	void Start ()
	{
/*		p1ScoreText = GetComponent<Text>();
		p2ScoreText = GetComponent<Text>();*/
		p1Score = 0;
		p2Score = 0;
		UpdateScore();
	}

	
	public void AddScore (int value, bool player) {
		if (player == true) // player 1
		{
			p1Score = Mathf.Max(0, p1Score + value);
		}
		else // player 2
		{
			p2Score = Mathf.Max(0, p2Score + value);
		}
		UpdateScore();
	}
	
	public void UpdateScore()
	{
		p1ScoreText.text = string.Format("{0}", p1Score).PadLeft(4, '0');
		p2ScoreText.text = string.Format("{0}", p2Score).PadLeft(4, '0');
	}
}
