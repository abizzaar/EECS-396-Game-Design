using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Code
{
    public class Slow : MonoBehaviour
    {
        //The enemy that possesses this component:
        public Enemy enemy;

        //Constant slow constant;
        private float slow = 0.75f;

        void Start()
        {
            if (gameObject.GetComponent<Enemy>() == null)
            {
                Debug.Log("Error: Slow Attached to Non-Enemy");
                return;
            }
            enemy = gameObject.GetComponent<Enemy>();
            enemy.gameObject.GetComponent<NavMeshAgent>().speed *= slow;
        }

        void OnDestroy()
        {
            if (gameObject.GetComponent<Enemy>() == null)
            {
                Debug.Log("Error: Slow Attached to Non-Enemy");
                return;
            }
            enemy = gameObject.GetComponent<Enemy>();
            enemy.gameObject.GetComponent<NavMeshAgent>().speed /= slow;
        }

    }
}