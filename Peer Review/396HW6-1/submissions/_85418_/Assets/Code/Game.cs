using UnityEngine;

namespace Code
{
    public class Game : MonoBehaviour
    {
        public static Game Ctx;

        /// <summary>
        /// The class that handles serialization/deserialization
        /// </summary>

        // 
        // all of the things that we can about saving/loading
        public static GridMap Grid;
        public static BulletSpawner Bullets;
        public static HealthBar Health;
        


        internal void Start () {
            Ctx = this;

            Grid = GameObject.Find("Grid").GetComponent<GridMap>();
            Health = GameObject.Find("Slider").GetComponent<HealthBar>();
            Bullets = new BulletSpawner(GameObject.Find("BulletSpawner").transform);

        }
    }
}