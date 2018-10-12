using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Spawner : MonoBehaviour
    {

        private Object _normalEnemy;
        private Grid spawnGrid = null;

        void Start()
        {
            _normalEnemy = Resources.Load("Prefabs/NormalEnemy");

            //Calls spawn function repeatedly
            InvokeRepeating("makeEnemy", 1, 3);
        }

        void Update()
        {
            /*this used to sometimes run before all the coordinates could get 
             * initialized in start, so I moved it to update and it should only 
             * run once. Feel free to update if you can think of a less hacky
             * way of doing this */
            if (spawnGrid == null)
            {
                Grid[] gridList = FindObjectsOfType<Grid>();
                foreach (Grid block in gridList)
                {
                    if (block.gridPoint.x == gameObject.transform.position.x &&
                        block.gridPoint.z == gameObject.transform.position.z)
                    {
                        spawnGrid = block;
                        break;
                    }
                }

            }
        }

        private void makeEnemy()
        {
            //Create and place new enemy object 
            GameObject newEnemy;
            newEnemy = (GameObject)Instantiate(_normalEnemy);
            spawnGrid.SnapToGrid(newEnemy);

            //Set hit points based on type and start movement
            Enemy e = newEnemy.GetComponent<Enemy>();
            e.Spawn(spawnGrid);
        }
        
    }
}
