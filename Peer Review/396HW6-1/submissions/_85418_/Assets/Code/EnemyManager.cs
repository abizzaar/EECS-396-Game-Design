
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code
{
    public class EnemyManager: MonoBehaviour
    {
        private const float SpawnTime = 3f;
        private static Object _enemyPrefab;
        private float _lastspawn;


        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            _enemyPrefab = Resources.Load("Prefabs/Enemy");
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            if ((Time.time - _lastspawn) < SpawnTime) return;
            _lastspawn = Time.time;
            SpawnEnemy();
        }

        private void SpawnEnemy () {
            GameObject newEnemy = (GameObject) Object.Instantiate(_enemyPrefab, transform.position, new Quaternion());
            newEnemy.transform.SetParent(transform);
            var enemyComp = newEnemy.GetComponent<Enemy>();
            enemyComp.Initialize();
           
        }
        
    }
}