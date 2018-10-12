using System.Collections;
using System.Collections.Generic;
using Assets.Code.Managers;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Code.Structure
{
    public class GridSquare : MonoBehaviour
    {
        public GameObject ShopPanel;
        public GameObject UpgradeShopPanel;
        public LayerMask lm;
        public GameObject Bases;

        private GameObject tower;
        private bool hover;
        private bool clicked;

        private Renderer rend;
        private Material startMat;
        public Material hoverMat;
        public Material selectMat;

        Shop shop;

        void Awake()
        {
            ShopPanel = GameObject.Find("ShopPanel");
            UpgradeShopPanel = GameObject.Find("UpgradeShopPanel");
        }

        void Start()
        {
            rend = GetComponent<Renderer>();
            startMat = rend.material;
            hover = false;
            clicked = false;
            shop = Shop.mart;
        }

        void OnMouseEnter()
        {
            hover = true;
            if (rend.material == startMat)
            {
                rend.material = hoverMat;
            }
        }

        void OnMouseExit()
        {
            if (!clicked)
            {
                rend.material = startMat;
            }
            hover = false;

        }
        
        void Update()
        {
            if (shop.getSelected() != gameObject)
                {
                    rend.material = startMat;
                }

        }


        void OnMouseDown()
        {
            if (clicked)
            {
                if (tower != null)
                {
                    CloseUpgrade();
                    return;
                }
                else
                {
                    CloseShop();
                    return;
                }
            }
            clicked = true;
            shop.setSelected(gameObject);
            //Do something when clicked
            if (tower != null)
            {
                //Open upgrade menu
                UpgradeShopPanel.gameObject.SetActive(true);
                //UpgradeShopPanel.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                rend.material = selectMat;
            }
            else
            {
                //Open build menu
                ShopPanel.gameObject.SetActive(true);
                //ShopPanel.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                rend.material = selectMat;
            }

        }

        public void CloseShop()
        {
            Debug.Log("Closed Shop Window");
            clicked = false;
            rend.material = startMat;
            ShopPanel.gameObject.SetActive(false);
        }

        public void CloseUpgrade()
        {
            Debug.Log("Closed Upgrade Window");
            clicked = false;
            rend.material = startMat;
            UpgradeShopPanel.gameObject.SetActive(false);
        }

        public void ObtainTurret(GameObject turret)
        {
            tower = turret;
        }

        public GameObject SendTurret()
        {
            return tower;
        }

        public bool CanIPlace()
        {
            //Box cast straight up (y-dir) to see if there is anything blocking a tower placement.
            // BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation = Quaternion.identity, 
            // float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal); 
            bool boxc = Physics.BoxCast(transform.position, GetComponent<BoxCollider>().size / 2, Vector3.up, Quaternion.identity, 5f,lm);
            return !boxc;
        }
        
    }
}