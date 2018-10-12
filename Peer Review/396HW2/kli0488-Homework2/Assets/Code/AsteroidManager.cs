using System;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Code
{
    /// <inheritdoc><cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Manager class for spawning and tracking all of the game's asteroids
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AsteroidManager : MonoBehaviour, ISaveLoad
    {
        private const float SpawnTime = 3f;
        private const int MaxAsteroidCount = 8;
        private static Object _asteroidPrefab;
        private float _lastspawn;
        private Transform _holder;

        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            _asteroidPrefab = Resources.Load("Asteroid");
            _holder = transform;
            Asteroid.Manager = this;
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            if ((Time.time - _lastspawn) < SpawnTime) return;
            _lastspawn = Time.time;
            Spawn();
        }

        private void Spawn () {
            if (_holder.childCount >= MaxAsteroidCount) { return; }

            var pos = BoundsChecker.GetRandomPos();
            var vel = BoundsChecker.GetRandomVelocity();
            int size = Random.Range(2, Asteroid.AsteroidTypes); // don't spawn tinies

            ForceSpawn(pos, vel, size);
        }

        // TODO fill me in
        public void ForceSpawn (Vector2 pos, Vector2 velocity, int size, Quaternion rotation = new Quaternion())
        {
            GameObject x = GameObject.Instantiate((GameObject) _asteroidPrefab, pos, rotation, _holder);
            x.GetComponent<Asteroid>().Initialize(velocity, size);
        }

        #region saveload

        // TODO fill me in
        public GameData OnSave () {
            AsteroidsData newRocks = new AsteroidsData();
            Asteroid[] oldRocks = (Asteroid[]) FindObjectsOfType(typeof(Asteroid)); 
            newRocks.Asteroids= new List<AsteroidData>();
            foreach (Asteroid rock in oldRocks)
                {
                    AsteroidData temp = new AsteroidData();
                    temp.Size = rock.GetComponent<Asteroid>().Size;
                    temp.Pos = rock.GetComponent<Rigidbody2D>().position;
                    temp.Velocity = rock.GetComponent<Rigidbody2D>().velocity;
                    newRocks.Asteroids.Add(temp);
                }
            return newRocks;
        }

        // TODO fill me in
        public void OnLoad (GameData data)
        {
            foreach (Transform rock in _holder)
            {
                Destroy(rock.gameObject);
            }
            AsteroidsData updateList = (AsteroidsData) data;
            foreach (AsteroidData update in updateList.Asteroids)
            {
                ForceSpawn(update.Pos, update.Velocity, update.Size);
            }
        }

        #endregion
    }

    /// <summary>
    /// The save data for all the asteroids
    /// </summary>
    public class AsteroidsData : GameData
    {
        public List<AsteroidData> Asteroids = new List<AsteroidData>();
    }

    /// <summary>
    /// The save data for one asteroid
    /// </summary>
    public class AsteroidData
    {
        public int Size;
        public Vector2 Pos;
        public Vector2 Velocity;
    }
}
