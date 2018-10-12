using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code
{
    //When added to an enemy, applies a Damage Over Time effect.  
    //Since there is only one source, the freeze tower, this DOT is a constant.
    //If multiple instances of Dot are on a single enemy, the effect is additive.
    public class Dot : MonoBehaviour
    {
        //The enemy that possesses this component:
        private Enemy enemy;

        //Constant DOT constant;
        private int dot = 10;
        

        void Start()
        {
            if (gameObject.GetComponent<Enemy>() == null)
            {
                Debug.Log("Error: DOT Attached to Non-Enemy");
                return;
            }
            enemy = gameObject.GetComponent<Enemy>();
            StartCoroutine("Damage");



        }

        void OnDestory()
        {
            StopCoroutine("Damage");
        }

        IEnumerator Damage()
        {
            while (true)
            {
                enemy.enemyhealth -= dot;
                yield return new WaitForSeconds(1f);
            }
        }
        
    }
}