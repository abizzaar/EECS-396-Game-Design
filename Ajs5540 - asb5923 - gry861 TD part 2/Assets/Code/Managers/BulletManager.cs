using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code.Managers
{
    /// <summary>
    /// Bullet manager for spawning and tracking all of the game's bullets
    /// </summary>
    public class BulletManager : MonoBehaviour
    {
        /*
        private readonly Transform _holder;

        /// <summary>
        /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
        /// </summary>
        private readonly Object _bullet;

        public BulletManager(Transform holder)
        {
            _holder = holder;
            _bullet = Resources.Load("Bullet");
        }

        public void ForceSpawn(Vector3 pos, Quaternion rotation, Vector3 velocity, float deathtime)
        {
            GameObject bull = (GameObject)Object.Instantiate(_bullet, pos, rotation);
            bull.transform.parent = _holder.transform;
            bull.GetComponent<Bullet>().Initialize(velocity, Time.time + Bullet.Lifetime);
            bull.GetComponent<Rigidbody>().velocity = bull.transform.forward * 10;
        }
        */
    }
}