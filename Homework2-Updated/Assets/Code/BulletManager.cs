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
			
        public void ForceSpawn (Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime) {
			GameObject newBullet = (GameObject)Object.Instantiate (_bullet, pos, rotation);
			newBullet.transform.parent = _holder;
			newBullet.GetComponent<Bullet> ().Initialize(velocity, deathtime);
        }

        #region saveload

        public GameData OnSave () {
			BulletsData allBulletsData = new BulletsData ();
			Object[] listOfBullets = Object.FindObjectsOfType (typeof(Bullet));
			foreach (var b in listOfBullets) {
				Bullet currBullet = (Bullet)b;
				BulletData bulletData = new BulletData ();
				Rigidbody2D rigidBody = currBullet.GetComponent<Rigidbody2D> ();
				bulletData.Pos = rigidBody.position;
				bulletData.Velocity = rigidBody.velocity;
				bulletData.Rotation = rigidBody.rotation;
				allBulletsData.Bullets.Add(bulletData);
			}
			return allBulletsData;
        }

        public void OnLoad (GameData data) {
			Object[] listOfBullets = Object.FindObjectsOfType (typeof(Bullet));
			foreach (var b in listOfBullets) {
				Bullet currBullet = (Bullet)b;
				Object.Destroy (currBullet.gameObject);
			}

			BulletsData allBulletsData = (BulletsData)data;
			foreach (var bulletData in allBulletsData.Bullets) {
				ForceSpawn (bulletData.Pos, Quaternion.Euler (0, 0, bulletData.Rotation), bulletData.Velocity, Time.time + Bullet.Lifetime);
			}
        }

        #endregion

    }

    /// <summary>
    /// Save data for all bullets in game
    /// </summary>
    public class BulletsData : GameData
    {
		public List<BulletData> Bullets = new List<BulletData> ();
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