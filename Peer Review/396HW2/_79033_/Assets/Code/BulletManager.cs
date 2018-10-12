using System;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Code
{
    /// <summary>
    /// Bullet manager for spawning and tracking all of the game's bullets
    /// </summary>
    public class BulletManager : ISaveLoad
    {
        private readonly Transform _holder;

        /// <summary>
        /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
        /// </summary>
        private readonly Object _bullet;

        public BulletManager (Transform holder) {
            _holder = holder;
            _bullet = Resources.Load("Bullet");
        }

        // TODO fill me in
        public void ForceSpawn (Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime)
        {
            
            GameObject Boolet = (GameObject)Object.Instantiate(_bullet, pos, rotation, _holder);
            Boolet.GetComponent<Bullet>().Initialize(velocity, deathtime);



        }

        #region saveload

        // TODO fill me in
        public GameData OnSave () {
            BulletsData Bulletstorm = new BulletsData();
            
            List<BulletData> chamber = new List<BulletData>();
            foreach (Bullet a in Game.FindObjectsOfType(typeof(Bullet)))
            {
                BulletData b = new BulletData();
                b.Pos = a.GetComponent<Rigidbody2D>().position;
                b.Velocity = a.GetComponent<Rigidbody2D>().velocity;
                b.Rotation = a.GetComponent<Rigidbody2D>().rotation;
                chamber.Add(b);
            }
            Bulletstorm.Bullets = chamber;
            return Bulletstorm;
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            foreach (Bullet b in Game.FindObjectsOfType(typeof(Bullet)))
            {
                Game.Destroy(b.gameObject);
            }
            BulletsData highnoon = (BulletsData)data;
            foreach (BulletData a in highnoon.Bullets)
            {
                ForceSpawn(a.Pos, new Quaternion(), a.Velocity, Time.time + Bullet.Lifetime);
            }
        }

        #endregion

    }

    /// <summary>
    /// Save data for all bullets in game
    /// </summary>
    public class BulletsData : GameData
    {
        public List<BulletData> Bullets;
    }

    /// <summary>
    /// Save data for a single bullet
    /// </summary>
    public class BulletData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
    }
}