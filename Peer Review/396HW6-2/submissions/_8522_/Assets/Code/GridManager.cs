using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {

    public GridCell activeCell;
    public GameObject buildMenu;
    public GameObject upgradeSellMenu;
    public GameObject tower;
    public int towerCost;
    public GameObject buildTowerButton;
    public Object simpleEnemy;

	// Use this for initialization
	void Start () {
        Draw5x5Grid();
        buildMenu = GameObject.Find("Canvas").transform.Find("BuildMenu").gameObject;
        upgradeSellMenu = GameObject.Find("Canvas").transform.Find("UpgradeSellMenu").gameObject;
        buildTowerButton = GameObject.Find("Canvas").transform.Find("BuildMenu").transform.Find("BuildTowerButton").gameObject;
	    simpleEnemy = Resources.Load("SimpleEnemy");
	}

    void Awake ()
    {
        Globals.gm = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (Globals.pm.money >= towerCost)
        {
            buildTowerButton.GetComponent<Button>().interactable = true;
        } else
        {
            buildTowerButton.GetComponent<Button>().interactable = false;
        }
	}

    void Draw5x5Grid()
    {
        Vector3 cellSize = new Vector3(1.0f, 0.2f, 1.0f);
        Vector3 currPos = new Vector3(0.0f, 0.0f, 0.0f);
        Transform parent = gameObject.transform;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                currPos.x += cellSize.x;
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = currPos;
                cube.transform.localScale = cellSize;
                cube.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                cube.AddComponent<GridCell>();
                cube.transform.SetParent(parent);
                cube.GetComponent<GridCell>().canBuild = true;
                if ((i == 0 && j == 0) || (i == 4 && j == 4))
                {
                    cube.GetComponent<GridCell>().canBuild = false;
                }
            }
            currPos.x = 0.0f; // Reset x position for each row
            currPos.z -= cellSize.z; // Subtracting makes it go down
        }
    }

    public void BuildTower()
    {
/*        if (Physics.BoxCast(activeCell.transform.position, new Vector3(0.5f, 0.1f, 0.5f), Vector3.up, Quaternion.identity, 1.0f))
        {
            buildMenu.transform.Find("warningText").GetComponent<Text>().text = "enemies in cell.";
            return;
        }
        
        buildMenu.active = false;
        Globals.pm.AddMoney(-towerCost);
        activeCell.BuildTower();
        activeCell = null;*/

        StartCoroutine("coBuilder");

    }

    public IEnumerator coBuilder()
    {
        if (Physics.BoxCast(activeCell.transform.position, new Vector3(0.5f, 0.1f, 0.5f), Vector3.up, Quaternion.identity, 1.0f))
        {
            buildMenu.transform.Find("warningText").GetComponent<Text>().text = "enemies in cell.";
            yield break;
        }
        
        GameObject testTower = Instantiate(Globals.gm.tower, new Vector3(activeCell.transform.position.x, 0.59f, activeCell.transform.position.z), activeCell.transform.rotation);
        testTower.GetComponent<MeshRenderer>().enabled = false;
        testTower.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        
        GameObject testEnemy = (GameObject)Object.Instantiate(simpleEnemy, Globals.em.transform.position, Globals.em.transform.rotation);
        testEnemy.GetComponent<MeshRenderer>().enabled = false;

        yield return null;


        if (testEnemy.GetComponent<NavMeshAgent>().pathStatus != NavMeshPathStatus.PathComplete)
        {
            buildMenu.transform.Find("warningText").GetComponent<Text>().text = "blocks path";
            Destroy(testTower);
            Destroy(testEnemy);
            yield break;
        }

        foreach (NavMeshAgent jBond in Globals.em.GetComponentsInChildren<NavMeshAgent>())
        {
            if (jBond.pathStatus != NavMeshPathStatus.PathComplete)
            {
                buildMenu.transform.Find("warningText").GetComponent<Text>().text = "blocks path";
                Destroy(testTower);
                Destroy(testEnemy);
                yield break;
            }
        }
        
        Destroy(testTower);
        Destroy(testEnemy);

        buildMenu.transform.Find("warningText").GetComponent<Text>().text = "";
        buildMenu.active = false;
        Globals.pm.AddMoney(-towerCost);
        activeCell.BuildTower();
        activeCell = null;


    }

    public void SellTower()
    {
        upgradeSellMenu.active = false;
        Globals.pm.AddMoney((int)Mathf.Round((float)towerCost * 0.9f));
        activeCell.SellTower();
        activeCell = null;
    }
}
