using System.Collections;
using Assets.Code.Managers;
using Assets.Code.Structure;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public class Shop : MonoBehaviour
    {
        public static Shop mart;

        public GameObject ShopPanel;
        public GameObject UpgradeShopPanel;
        public GameObject Base;

        public GameObject NormalTowerButton;

        public int Gold;
        private static Text _goldText;
        public GameObject NormalTowerPrefab;
        public GameObject FreezeTowerPrefab;
        public GameObject ShockTowerPrefab;
        private GameObject SelectedGridSquare;
        public Vector3 positionOffset;
        private Pathchecker pathchecker;
        private GameObject _turret;

        void Awake()
        {
            if (mart != null)
            {
                Debug.Log("More than one shop.  Error!");
                return;
            }
            mart = this;
        }

        void Start()
        {
            ShopPanel = GameObject.Find("ShopPanel");
            UpgradeShopPanel = GameObject.Find("UpgradeShopPanel");
            Base = GameObject.Find("Pathchecker");
            //ShopPanel.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            //UpgradeShopPanel.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            ShopPanel.gameObject.SetActive(false);
            UpgradeShopPanel.gameObject.SetActive(false);
            _goldText = GameObject.Find("GoldText").GetComponent<Text>();
            pathchecker = Base.GetComponent<Pathchecker>();

        }

        void Update()
        {
            if (Gold < 500)
            {
                NormalTowerButton.GetComponent<Button>().interactable = false;
            }
            if (Gold >= 500)
            {
                NormalTowerButton.GetComponent<Button>().interactable = true;
            }

        }

        public void setSelected(GameObject node)
        {
            SelectedGridSquare = node;
        }

        public GameObject getSelected()
        {
            return SelectedGridSquare;
        }

        public void PurchaseNormalTower()
        {
            //No Gold
            if (Gold < 500)
            {
                Debug.Log("Not enough gold!");
                return;
            }

            //Enemy Ontop
            if (!SelectedGridSquare.GetComponent<GridSquare>().CanIPlace())
            {
                Debug.Log("Enemy on top!");
                return;
            }

            //Path Impossible

            _turret = (GameObject)Instantiate(NormalTowerPrefab, SelectedGridSquare.transform.position + positionOffset, Quaternion.identity);

            StartCoroutine(Waitaframe());

            
            
        }

        public void PurchaseFreezeTower()
        {
            Vector3 posO = new Vector3(0, 0.4f, 0);
            //No Gold
            if (Gold < 750)
            {
                Debug.Log("Not enough gold!");
                return;
            }

            //Enemy Ontop
            if (!SelectedGridSquare.GetComponent<GridSquare>().CanIPlace())
            {
                Debug.Log("Enemy on top!");
                return;
            }

            //Path Impossible
            _turret = (GameObject)Instantiate(FreezeTowerPrefab, SelectedGridSquare.transform.position + posO, Quaternion.identity * Quaternion.Euler(Vector3.up * 180));

            StartCoroutine(Waitaframefreeze());
            

            
        }

        public void PurchaseShockTower()
        {
            //No Gold
            if (Gold < 900)
            {
                Debug.Log("Not enough gold!");
                return;
            }

            //Enemy Ontop
            if (!SelectedGridSquare.GetComponent<GridSquare>().CanIPlace())
            {
                Debug.Log("Enemy on top!");
                return;
            }

            //Path Impossible

            _turret = (GameObject)Instantiate(ShockTowerPrefab, SelectedGridSquare.transform.position + positionOffset, Quaternion.identity);

            StartCoroutine(Waitaframeshock());



        }


        public void DestroyTower()
        {
            GameObject _turret = SelectedGridSquare.GetComponent<GridSquare>().SendTurret();
            if (_turret == null)
            {
                Debug.Log("Error no turrent to be deleted");
            }
            if (_turret.gameObject.GetComponent<NormalTower>() != null)
            {
                Gold += 450;
                Debug.Log("Normal Tower Destroyed");
            }
            else if (_turret.gameObject.GetComponent<Freezer>() != null)
            {
                Gold += 675;
                Debug.Log("Freeze Tower Destroyed");
            }
            //Lightning Tower Here

            Object.Destroy(_turret);
            _goldText.text = Gold.ToString();
            SelectedGridSquare.GetComponent<GridSquare>().CloseUpgrade();
        }

        public void UpgradeTower()
        {
            Debug.Log("Tower Upgraded");
            _goldText.text = Gold.ToString();
        }

        public void AlterGold(int value)
        {
            Gold += value;
            _goldText.text = Gold.ToString();
        }

        IEnumerator Waitaframe()
        {
            
            yield return 0;
            if (!pathchecker.clearpath)
            {
                Debug.Log("Blocks path to Base!");
                Object.Destroy(_turret);
            }
            else
            {
                Debug.Log("Normal Tower Purchased");
                Gold -= 500;
                SelectedGridSquare.GetComponent<GridSquare>().ObtainTurret(_turret);
                _goldText.text = Gold.ToString();
                SelectedGridSquare.GetComponent<GridSquare>().CloseShop();
            }
        }

        IEnumerator Waitaframefreeze()
        {

            yield return 0;
            if (!pathchecker.clearpath)
            {
                Debug.Log("Blocks path to Base!");
                Object.Destroy(_turret);
            }
            else
            {
                Debug.Log("Freeze Tower Purchased");
                Gold -= 750;
                SelectedGridSquare.GetComponent<GridSquare>().ObtainTurret(_turret);
                _goldText.text = Gold.ToString();
                SelectedGridSquare.GetComponent<GridSquare>().CloseShop();
            }
        }
        IEnumerator Waitaframeshock()
        {

            yield return 0;
            if (!pathchecker.clearpath)
            {
                Debug.Log("Blocks path to Base!");
                Object.Destroy(_turret);
            }
            else
            {
                Debug.Log("Shock Tower Purchased");
                Gold -= 900;
                SelectedGridSquare.GetComponent<GridSquare>().ObtainTurret(_turret);
                _goldText.text = Gold.ToString();
                SelectedGridSquare.GetComponent<GridSquare>().CloseShop();
            }
        }


    }
    
}
