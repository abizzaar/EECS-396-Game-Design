using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Cell : MonoBehaviour
    {
        private bool _hasTower;
        private bool _end;
        private Renderer rend;
        public Color hoverColor;
        private Color startColor;
        private Color defaultButtonColor;
        public GameObject bMenu;
        private Vector3 cubePos;
        public GameManager gManager;
        private GameObject g;
        public GameObject fText, tText;
        public GameObject Test;
        private NavMeshPath testPath;


        // Use this for initialization
        void Start()
        {
            _hasTower = false;
            _end = false;
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;
            defaultButtonColor = bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>()
                .GetComponent<Image>().color;
            //cubePos = Camera.main.WorldToScreenPoint(this.transform.position);
            //bMenu.transform.position = cubePos;
            bMenu.SetActive(false);
            fText.SetActive(false);
            tText.SetActive(false);
            g = GameObject.Find("Game Manager");
            gManager = g.GetComponent<GameManager>();
            testPath = new NavMeshPath();
            //bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().onClick.AddListener(towerSpawn);
            //bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().onClick.AddListener(sellTower);
        }

        public void gameOver()
        {
            _end = true;
        }

        void OnMouseEnter()
        {
            if (!_end)
            {
                if (!gManager.menuOpen)
                    rend.material.color = hoverColor;
            }
        }

        void OnMouseExit()
        {
            if(!gManager.menuOpen)
                rend.material.color = startColor;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_end)
            {
                GetComponent<NavMeshObstacle>().enabled = _hasTower;
            }
        }
        IEnumerator Flash()
        {
            rend.material.color = Color.red; //Too tired, please pick a better color
            yield return new WaitForSeconds(1.0f);
            rend.material.color = Color.white;
        }

        //This will be sufficient for detecting mouse clicks, in the future a menu will be added to select towers
        void OnMouseUp()
        {
            if (!_end)
            {
                if (gManager.menuOpen)
                {
                    bMenu.SetActive(false);
                    gManager.menuOpen = false;
                    return;
                }
                //Debug.Log("Hi! My name is: (" + transform.position.x + ", " + transform.position.z + ")");
                if (!_hasTower)
                {
                    /*StartCoroutine(TowerCheck());
                    if (testPath.status != NavMeshPathStatus.PathComplete)
                    {
                        Flash();
                        GetComponent<NavMeshObstacle>().enabled = false;
                        return;
                    }
                    GetComponent<NavMeshObstacle>().enabled = false;*/
                    bMenu.SetActive(true);
                    //bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = defaultButtonColor;
                    //bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().enabled = true;
                    if (gManager.Gold >= 10)
                    {
                        bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().interactable = false;
                    }
                    //bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.black;
                    //bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().enabled = false;
                    bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().interactable = false;
                    //bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.black;
                    //bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().enabled = false;
                    bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().interactable = false;
                    gManager.menuOpen = true;
                }

                if (!gManager.menuOpen)
                {
                    bMenu.SetActive(true);
                    //bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.black;
                    //bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().enabled = false;
                    bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().interactable = false;
                    //bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = defaultButtonColor;
                    //bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().enabled = true;
                    bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().interactable = true;
                    //bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = defaultButtonColor;
                    //bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().enabled = true;
                    bMenu.transform.Find("sellButton").gameObject.GetComponent<Button>().interactable = true;
                    gManager.menuOpen = true;
                    //Debug.Log(gManager);
                }
                //_hasTower = true;   
            }
        }

        private IEnumerator TowerCheck()
        {
            GetComponent<NavMeshObstacle>().enabled = true;
            yield return new WaitForEndOfFrame();
            Test.GetComponent<NavMeshAgent>()
                .CalculatePath(FindObjectOfType<Base>().gameObject.transform.position, testPath);
            Debug.Log(testPath.status);
        }

        public void towerSpawn()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                if (gameObject.GetComponent<Collider>().bounds
                    .Intersects(enemy.gameObject.GetComponent<Collider>().bounds))
                {
                    return;
                }
            }

            Vector3 pos = gameObject.transform.position;
            pos.y = pos.y + 0.7f;
            if(gManager.Gold >= 10 && !_hasTower)
            {
                gManager.SpendGold(10);
                GameObject tower = Instantiate(Resources.Load("Tower"), pos, new Quaternion(0, 0, 0, 0), gameObject.transform.parent) as GameObject;
                tower.transform.parent = gameObject.transform;
                _hasTower = true;
                gManager.LastCell = this.gameObject;
                gManager.menuOpen = false;
                bMenu.SetActive(false);
            }
            else if (gManager.Gold < 10)
            {
                StartCoroutine(noMoney());
            }
            else
            {
                StartCoroutine(isBuilt());
            }
            
        }

        public void sellTower()
        {

            if (_hasTower)
            {
                foreach(Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                gManager.AddGold(9);
                gManager.menuOpen = false;
                bMenu.SetActive(false);
                _hasTower = false;
                
            }

        }

        public void invalidTower()
        {

            if (_hasTower)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                gManager.AddGold(10);
                gManager.menuOpen = false;
                bMenu.SetActive(false);
                _hasTower = false;

            }

        }


        IEnumerator noMoney()
        {
            fText.SetActive(true);
            yield return new WaitForSeconds(3);
            fText.SetActive(false);
        }
        
        IEnumerator isBuilt()
        {
            tText.SetActive(true);
            yield return new WaitForSeconds(3);
            tText.SetActive(false);
        }

    }
}