using UnityEngine;

namespace Assets.Code.Structure
{
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// The game context.
        /// A pointer to the currently active game (so that we don't have to use something slow like "Find").
        /// </summary>
        public static Game Ctx;

        // 
        // all of the things that we can about saving/loading
        public static ScoreManager Score;
        public static ScoreManager Score2;
        public static Player Player;
        public static Player Player2;
        public static BulletManager Bullets;


        internal void Start () {
            Ctx = this;

            Score = GameObject.Find("ScoreText1").GetComponent<ScoreManager>();
            Score2 = GameObject.Find("ScoreText2").GetComponent<ScoreManager>();
            Player = GameObject.Find("Player1").GetComponent<Player>();
            Player2 = GameObject.Find("Player2").GetComponent<Player>();
            Bullets = new BulletManager(GameObject.Find("Bullets").transform);

        }

        private static bool IsMac () {
            return Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.OSXPlayer;
        }
    }
}