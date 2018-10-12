using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Grid : MonoBehaviour
    {
        private float _gx, _gy, _gz;
        public Vector3 gridPoint; 

        public List<Grid> neighbors = new List<Grid>();

        // Use this for initialization
        void Start()
        {
            //Finds point at top and center of grid cube
            Mesh gridMesh = gameObject.GetComponent<MeshFilter>().mesh;
            _gx = gameObject.transform.position.x;
            _gy = gridMesh.bounds.max.y;
            _gz = gameObject.transform.position.z;

            gridPoint = new Vector3(_gx, _gy, _gz);

            getNeighbors();

        }

        // Update is called once per frame
        void Update()
        {
        }

        private void getNeighbors()
        //Compiles a list of neighboring blocks
        {
            Grid[] gridList = FindObjectsOfType<Grid>();
            foreach (Grid block in gridList)
            {
                //Only adds vertical/horizontal neighbors to list
                if (((_gx == block.gridPoint.x + 10 || _gx == block.gridPoint.y - 10) && _gz == block.gridPoint.z)
                    || (_gx == block.gridPoint.x && (_gz == block.gridPoint.z + 10 || _gz == block.gridPoint.z - 10)))
                {
                    neighbors.Add(block);
                }
            }
        }

        public void SnapToGrid(GameObject g)
        //Snaps game object to center and top of grid block. Should work for all towers/enemy spawns
        {
            Mesh objectMesh = g.GetComponent<MeshFilter>().mesh;
            float yGap = g.transform.position.y - objectMesh.bounds.min.y;
            g.transform.position = new Vector3(_gx,_gy+yGap, _gz);
        }
    }


}