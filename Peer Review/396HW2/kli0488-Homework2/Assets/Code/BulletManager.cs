using System;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEditor;
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
            GameObject x = GameObject.Instantiate((GameObject) _bullet, pos, rotation, _holder);
            x.GetComponent<Bullet>().Initialize(velocity, deathtime);

        }

        #region saveload

        // TODO fill me in
        public GameData OnSave () {
            BulletsData newBullets = new BulletsData();
            newBullets.Bullets = new List<BulletData>();
            Bullet[] oldBullets = (Bullet[]) Object.FindObjectsOfType(typeof(Bullet));
            foreach (Bullet shrapnel in oldBullets)
                {
                    BulletData temp = new BulletData();
                    temp.Pos = shrapnel.GetComponent<Rigidbody2D>().position;
                    temp.Velocity = shrapnel.GetComponent<Rigidbody2D>().velocity;
                    temp.Rotation = shrapnel.GetComponent<Rigidbody2D>().rotation;
                    newBullets.Bullets.Add(temp);
                }
            return newBullets;
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            foreach (Transform shrapnel in _holder)
            {
                GameObject.Destroy(shrapnel.gameObject);
            }
            BulletsData updateList = (BulletsData) data;
            foreach (BulletData update in updateList.Bullets)
            {
                ForceSpawn(update.Pos, Quaternion.Euler(0,0,update.Rotation), update.Velocity, Time.time+ Bullet.Lifetime);
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