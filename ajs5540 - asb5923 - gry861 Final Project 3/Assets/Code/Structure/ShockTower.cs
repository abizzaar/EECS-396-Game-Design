using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Assets.Code.Structure;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

namespace Assets.Code.Structure
{
    public class ShockTower : MonoBehaviour
    {
        
        /*Checklist
        * 1. First check whether an enemy is colliding with box collider
        * 2. If there is an enemy start rotating towards the enemy
        * 3. Connect chain to first enemy
        * 4. Check for second enemy within one cell of first enemy. If that exists, connect chain
        * 5. Check for third enemy within one cell of second enemy. If that exists, connect chain
        * 6. Shock all enemies until first enemy is out of box collider area
        */

     

        public string enemyTag = "Enemy";

        
        
        public Transform firePoint;
        
        private bool currentlyShocking = false;
        private bool shockingSecond = false;
        private bool shockingThird = false;
        private GameObject enemyCurrentlyShocking = null;
        private GameObject secondEnemy = null;
        private GameObject thirdEnemy = null;
        private Camera camera;
        public float colliderRadius = 1f;
        public LayerMask layerMask;
        LineRenderer firstChain;
        private Material lightningMaterial;
        
        void Start()
        {
            camera = GameObject.Find("Camera").GetComponent<Camera>();
            lightningMaterial = new Material(Shader.Find("Unlit/Texture"));
            firstChain = gameObject.AddComponent<LineRenderer>();
            CustomizeLine(firstChain);
            firstChain.positionCount = 0;
            
        }

        void Update()
        {
            if (!currentlyShocking)
            {
                Enemy[] listOfEnemies = FindObjectsOfType<Enemy>();
                foreach (var x in listOfEnemies)
                {
                    GameObject go = x.gameObject;
                    float mag = (transform.position - go.transform.position).magnitude;
                    if (mag < 1.3)
                    {
                        enemyCurrentlyShocking = go;
                        currentlyShocking = true;
                        enemyCurrentlyShocking.GetComponent<Enemy>().enemyhealth -= 30;
                        StartCoroutine(Shock());
                        break;
                    }
                }
            }
            else if (enemyCurrentlyShocking == null)
            {
                currentlyShocking = false;
            }
            else
            {
                float mag = (transform.position - enemyCurrentlyShocking.transform.position).magnitude;
                if (mag > 1.3) currentlyShocking = false;
            }
        }


        IEnumerator Shock()
        {         
            firstChain.positionCount = 2;
            
            while (currentlyShocking && enemyCurrentlyShocking != null)
            {
                // update first chain line
                firstChain.SetPosition(0, firePoint.position);
                firstChain.SetPosition(1, enemyCurrentlyShocking.transform.position);
                
                
                
                
                // check for nearby enemy
                if (!shockingSecond || secondEnemy == null)
                {
                    Enemy[] listOfEnemies = FindObjectsOfType<Enemy>();
                    foreach (var x in listOfEnemies)
                    {
                        GameObject go = x.gameObject;
                        if (go.gameObject.GetInstanceID() != enemyCurrentlyShocking.GetInstanceID())
                        {
                            float mag = (enemyCurrentlyShocking.transform.position - go.transform.position).magnitude;
                            if (mag < colliderRadius)
                            {
                                secondEnemy = go;
                                shockingSecond = true;
                                secondEnemy.GetComponent<Enemy>().enemyhealth -= 20;
                                firstChain.positionCount = 3;
                                firstChain.SetPosition(2, secondEnemy.transform.position);

                            }
                        }
                    }
                }
                else
                {
                    if (!shockingThird || thirdEnemy == null)
                    {
                        Enemy[] listOfEnemies = FindObjectsOfType<Enemy>();
                        foreach (var x in listOfEnemies)
                        {
                            GameObject go = x.gameObject;
                            if (go.gameObject.GetInstanceID() != enemyCurrentlyShocking.GetInstanceID()
                                && go.gameObject.GetInstanceID() != secondEnemy.GetInstanceID())
                            {
                                float mag = (secondEnemy.transform.position - go.transform.position).magnitude;
                                if (mag < colliderRadius)
                                {
                                    thirdEnemy = go;
                                    shockingThird = true;
                                    thirdEnemy.GetComponent<Enemy>().enemyhealth -= 10;
                                    firstChain.positionCount = 4;
                                    firstChain.SetPosition(3, thirdEnemy.transform.position);

                                }
                            }
                        }
                    }
                    else
                    {
                        float thirdMag = (secondEnemy.transform.position - thirdEnemy.transform.position).magnitude;
                        if (thirdMag > colliderRadius)
                        {
                            firstChain.positionCount = 3;
                            shockingThird = false;
                            thirdEnemy = null;
                        }
                        else firstChain.SetPosition(3, thirdEnemy.transform.position);
                    }
                    
                    float secondMag = (enemyCurrentlyShocking.transform.position - secondEnemy.transform.position).magnitude;
                    if (secondMag > colliderRadius)
                    {
                        firstChain.positionCount = 2;
                        shockingSecond = false;
                        secondEnemy = null;
                        shockingThird = false;
                        thirdEnemy = null;
                    }
                    else firstChain.SetPosition(2, secondEnemy.transform.position);
                }
                
                yield return null;
            }
            firstChain.positionCount = 0;
            shockingSecond = false;
            shockingThird = false;
            enemyCurrentlyShocking = null;
            secondEnemy = null;
            thirdEnemy = null;
        }

        

        
        private void CustomizeLine(LineRenderer line)
        {
            line.material = lightningMaterial;
            line.startColor = Color.white;
            line.endColor = Color.white;
            line.startWidth = 0.04f;
            line.endWidth = 0.04f;
        }
       
       

    }
}