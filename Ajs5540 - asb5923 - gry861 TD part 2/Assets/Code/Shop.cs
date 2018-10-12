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
        public GameObject Spawn;

        public GameObject NormalTowerButton;

        public int Gold;
        private static Text _goldText;
        public GameObject NormalTowerPrefab;
        private GameObject SelectedGridSquare;
        public Vector3 positionOffset;

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
            Spawn = GameObject.Find("Spawner");
            //ShopPanel.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            //UpgradeShopPanel.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            ShopPanel.gameObject.SetActive(false);
            UpgradeShopPanel.gameObject.SetActive(false);
            _goldText = GameObject.Find("GoldText").GetComponent<Text>();
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
            GameObject _turret = (GameObject)Instantiate(NormalTowerPrefab, SelectedGridSquare.transform.position + positionOffset, Quaternion.identity);
            /*
            if (!Spawn.GetComponent<Spawner>().clearpath)
            {
                Debug.Log("Blocks path to Base!");
                Object.Destroy(_turret);
                return;
            }
            */
            

            Debug.Log("Normal Tower Purchased");
            Gold -= 500;
            SelectedGridSquare.GetComponent<GridSquare>().ObtainTurret(_turret);
            _goldText.text = Gold.ToString();
            SelectedGridSquare.GetComponent<GridSquare>().CloseShop();
        }

        public void DestroyTower()
        {
            GameObject _turret = SelectedGridSquare.GetComponent<GridSquare>().SendTurret();
            if (_turret == null)
            {
                Debug.Log("Error no turrent to be deleted");
            }
            Object.Destroy(_turret);
            Gold += 450;
            Debug.Log("Tower Destroyed");
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


    }
}
