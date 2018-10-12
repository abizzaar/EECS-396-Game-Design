using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {

    public bool canBuild;
    private GameObject currentTower;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildTower()
    {
        currentTower = Instantiate(Globals.gm.tower, new Vector3(transform.position.x, 0.59f, transform.position.z), transform.rotation);
    }

    public void SellTower()
    {
        Destroy(currentTower);
    }

    private void OnMouseDown()
    {
        if (!canBuild) { return; }
        if (Globals.gm.activeCell == this)
        {
            Globals.gm.upgradeSellMenu.active = false;
            Globals.gm.buildMenu.active = false;
            Globals.gm.activeCell = null;
            return;
        }

        Globals.gm.activeCell = this;
        if (currentTower != null)
        {
            Globals.gm.upgradeSellMenu.active = true;
            Globals.gm.buildMenu.active = false;
        }
        else
        {
            Globals.gm.buildMenu.active = true;
            Globals.gm.upgradeSellMenu.active = false;

        }
    }
}
