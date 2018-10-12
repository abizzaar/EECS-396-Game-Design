using UnityEngine;
using Object = UnityEngine.Object;

namespace Code
{
    public class BulletSpawner : MonoBehaviour
    {
        
        private static Object _bulletPrefab;
        private Transform _holder;

        // ReSharper disable once UnusedMember.Global
        public BulletSpawner (Transform holder) {
            _holder = holder;
            _bulletPrefab = Resources.Load("Prefabs/Bullet");
        }
        

        public void ForceSpawn (Vector3 pos, Quaternion rotation, Vector3 bulletVelocity, Enemy target)
        {
            
            GameObject newBull = (GameObject) Instantiate(_bulletPrefab, pos, rotation);
            newBull.transform.SetParent(_holder);
            var bullComp = newBull.GetComponent<Bullet>();
            bullComp.Initialize(bulletVelocity, target, Bullet.Lifetime);

        }
    }
}