using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public int money;
    public Text moneyText;

	// Use this for initialization
	void Start () {
        money = 3000;
        moneyText.text = "$$$: " + money;
    }

    void Awake()
    {
        Globals.pm = this;    
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AddMoney(int value)
    {
        money += value;
        moneyText.text = "$$$: " + money;
    }

    public void forDaWin()
    {
        Globals.em.gameObject.SetActive(false);
        Globals.gm.gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("gameEndingInfo").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("gameEndingInfo").transform.Find("endText").GetComponent<Text>().text =
            "YOU WIN";
    }

    public void forDaLose()
    {
        Globals.em.gameObject.SetActive(false);
        Globals.gm.gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("gameEndingInfo").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("gameEndingInfo").transform.Find("endText").GetComponent<Text>().text =
            "YOU LOSE";
    }
}
