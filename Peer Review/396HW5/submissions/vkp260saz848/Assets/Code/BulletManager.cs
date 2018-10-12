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
    public class BulletManager
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
        public void ForceSpawn (Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime, string name)
        {
            GameObject b=(GameObject) Object.Instantiate(_bullet,pos,rotation,_holder);
            Bullet bullet = b.GetComponent<Bullet>();
            SpriteRenderer renderer = b.GetComponent<SpriteRenderer>();
            Color newcol = new Color(1, 1, 1);
            if (name == "Player1")
            {
                newcol = new Color(1, (float)129 / 255, (float)179 / 255);
            }
            else
            {
                newcol = new Color((float)132 / 255, (float)246 / 255, (float)128 / 255);
            }
            renderer.color = newcol;
            bullet.Initialize(velocity,deathtime,name);
        }
        
    }
}