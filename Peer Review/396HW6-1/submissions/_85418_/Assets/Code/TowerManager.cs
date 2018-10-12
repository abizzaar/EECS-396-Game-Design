using UnityEngine;
using Object = UnityEngine.Object;

namespace Code
{
    public class TowerManager: MonoBehaviour
    {
        private static Object _towerPrefab;
        
        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            _towerPrefab = Resources.Load("Prefabs/Tower");
            transform.position = new Vector3(2.5f, .1f, 2.5f);
            makeTower(transform.position);
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
           
        }

        private void makeTower(Vector3 position)
        {
            GameObject tower = (GameObject) Instantiate(_towerPrefab, position, new Quaternion());
            tower.transform.SetParent(transform);
            var towerComp = tower.GetComponent<Tower>();
            towerComp.Initialize();
        }
    }
}