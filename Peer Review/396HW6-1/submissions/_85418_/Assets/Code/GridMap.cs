using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    //Always a square grid
    public int GridSize = 5;

    private GameObject[,] grid;

    // Use this for initialization
    internal void Start()
    {
        BuildGrid();
    }

    // Update is called once per frame
    internal void Update()
    {

    }

    public void BuildGrid()
    {
        grid = new GameObject[GridSize, GridSize];

        //Align the bottom-left corner of the top of the first cube to the origin (when camera.forward faces z+).
        Vector3 scale       = transform.localScale;
        Vector3 alignAnchor = new Vector3(scale.x * 0.5f, scale.y * -0.5f, scale.z * 0.5f);
        Vector3 pos         = transform.position + alignAnchor;

        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                //Saving each gameObject...just in case?
                grid[x, z] = Instantiate(Resources.Load("Prefabs/Cell"),
                             new Vector3( pos.x + (x * scale.x), pos.y, pos.z + (z * scale.z) ),
                             transform.rotation,
                             transform) as GameObject;
            }
        }
    }
    //For debugging
    public void ClearGrid()
    {
        while (transform.childCount != 0)
        {
            foreach (Transform cell in transform)
            {
                if (cell != transform.root)
                {
                    DestroyImmediate(cell.gameObject);
                }
            }
        }
    }

    public GameObject getCell(int x, int y)
    {
        return grid[x, y];
    }

}

