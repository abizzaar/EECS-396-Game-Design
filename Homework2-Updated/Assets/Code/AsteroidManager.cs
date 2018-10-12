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
			
        public void ForceSpawn (Vector2 pos, Vector2 velocity, int size, Quaternion rotation = new Quaternion()) {
			var newAsteroid = (GameObject) Object.Instantiate (_asteroidPrefab, pos, rotation);
			newAsteroid.GetComponent<Asteroid> ().Initialize (velocity, size);
			newAsteroid.transform.parent = _holder;
        }

        #region saveload

        public GameData OnSave () {
			
			AsteroidsData allAsteroidsData = new AsteroidsData ();
			Object[] listOfAsteroids = FindObjectsOfType (typeof(Asteroid));
			foreach (var a in listOfAsteroids) {
				Asteroid currAsteroid = (Asteroid)a;
				AsteroidData asteroidData = new AsteroidData ();
				Rigidbody2D rigidBody = currAsteroid.GetComponent<Rigidbody2D> ();
				asteroidData.Size = currAsteroid.Size;
				asteroidData.Pos = rigidBody.position;
				asteroidData.Velocity = rigidBody.velocity;
				allAsteroidsData.Asteroids.Add(asteroidData);
			}
			return allAsteroidsData;
        }
			
        public void OnLoad (GameData data) {
			Object[] listOfAsteroids = FindObjectsOfType (typeof(Asteroid));
			foreach (var a in listOfAsteroids) {
				Asteroid currAsteroid = (Asteroid)a;
				Destroy (currAsteroid.gameObject);
			}
			AsteroidsData allAsteroidsData = (AsteroidsData)data;
			foreach (var asteroidData in allAsteroidsData.Asteroids) {
				ForceSpawn (asteroidData.Pos, asteroidData.Velocity, asteroidData.Size);
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
