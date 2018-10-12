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
            GameObject bullet = Object.Instantiate((GameObject) _bullet, pos, rotation, _holder);
            bullet.GetComponent<Bullet>().Initialize(velocity, deathtime);
        }

        #region saveload

        // TODO fill me in
        public GameData OnSave ()
        {
            Bullet[] bullets = Object.FindObjectsOfType(typeof(Bullet)) as Bullet[];
            BulletsData bData = new BulletsData();
            bData.Bullets = new List<BulletData>();
            foreach (Bullet bullet in bullets)
            {
                BulletData b = new BulletData();
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                b.Pos = rb.position;
                b.Velocity = rb.velocity;
                b.Rotation = rb.rotation;
                bData.Bullets.Add(b); 
            }
            return bData;
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            while (_holder.childCount > 0)
            {
                Transform child = _holder.GetChild(0);
                child.parent = null;
                Object.Destroy(child.gameObject);
            }
            BulletsData allData = (BulletsData) data;
            foreach (BulletData b in allData.Bullets)
            {
                Quaternion q = Quaternion.Euler(0, 0, b.Rotation);
                ForceSpawn(b.Pos, q, b.Velocity, Time.time + Bullet.Lifetime);
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