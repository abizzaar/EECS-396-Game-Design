using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Structure
{
    public class Freezer : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.GetComponent<Enemy>() != null)
            {
                Debug.Log("Enemy Collision");
                col.gameObject.AddComponent<Slow>();
                Debug.Log("Slow added");
                col.gameObject.AddComponent<Dot>();
                Debug.Log("Dot added");
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.GetComponent<Enemy>() != null)
            {
                Debug.Log("Enemy Collision Exit");
                Object.Destroy(col.gameObject.GetComponent<Slow>());
                Debug.Log("Slow removed");
                Object.Destroy(col.gameObject.GetComponent<Dot>());
                Debug.Log("Dot removed");
            }

        }
    }
}
