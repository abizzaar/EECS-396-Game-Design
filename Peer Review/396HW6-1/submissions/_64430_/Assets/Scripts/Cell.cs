using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class Cell : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //This will be sufficient for detecting mouse clicks, in the future a menu will be added to select towers
        void OnMouseUp()
        {
            Debug.Log("Hi! My name is: (" + transform.position.x + ", " + transform.position.y + ")");
        }
    }
}