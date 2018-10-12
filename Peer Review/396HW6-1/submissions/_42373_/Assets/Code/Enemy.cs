using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime.DynamicDispatching;
using UnityEngine;

namespace Assets.Code
{
    public class Enemy : MonoBehaviour
    {
       
        public Grid currentGrid;

        //hit points
        private int HP;

        //this will serve as a multiplier when pathfinding/diff enemies become a thing
        public int speed;


        public void Spawn(Grid spawnPoint)
        {
            currentGrid = spawnPoint;

            //damage and HP based on memory type
            if (gameObject.tag == "normalEnemy")
            {
                HP = 100;
                speed = 1;
            }

            MoveEnemy();
        }

        /* Linear movement. Accidentally started programming pathfinding so the
         * uncommented code is the nerfed linear movement and the commented code
         * is the precursor to whatever our pathfinding will wind up being */

        private void MoveEnemy()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            Base b = FindObjectOfType<Base>();

            Vector3 direction = new Vector3(b.transform.position.x - currentGrid.gridPoint.x,
                0, b.transform.position.z - currentGrid.gridPoint.z);

            rb.AddForce(speed*direction);

            /* 
            if (currentGrid != goalGrid)
            {
                Grid NextGrid = currentGrid;

                //iterate through all neighboring grid boxes
                foreach (Grid n in currentGrid.neighbors)
                {
                    //skip this one if it gets you further away
                    if ((Mathf.Abs(goalGrid.gx - currentGrid.gx) < Mathf.Abs(goalGrid.gx - n.gx))
                        || (Mathf.Abs(goalGrid.gz - currentGrid.gz) < Mathf.Abs(goalGrid.gz - n.gz)))
                    {
                        continue;
                    }

                    //pick goal for sure if it's nearby
                    if (n.gx == goalGrid.gx && n.gz == goalGrid.gz)
                    {
                        NextGrid = n;
                        break;
                    }

                    //otherwise hold it for now and check the alternatives
                    else
                    {
                        NextGrid = n;
                    }
                }          
            }*/
        }

        private void OnCollisionEnter(Collision coll)
        {
            //if it collides with bullet, decrease health
            if (coll.gameObject.GetComponent<Bullet>())
            {
                HP = HP - 25;

                //die if hit points run out
                if (HP <= 0)
                {
                    Destroy(gameObject);
                }
            }

            //die regardless if you hit tower or base
            else
            {
                Destroy(gameObject);
            }
        }

    }
}

