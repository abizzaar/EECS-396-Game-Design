using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class UserInterface : MonoBehaviour
    {
        private Button _quit;
        private static Text _scoreText;
        public static int Score { get; private set; }

        internal void Start () {
            _quit = GameObject.Find("Quit").GetComponent<Button>();
            _scoreText = GameObject.Find("Score").GetComponent<Text>();

            // reset the score
            Score = 0;
            UpdateScore();

            // initialize the quit button
            InitializeQuit();
        }


        //
        // Fill in these two functions

        private void InitializeQuit () {
            // add a listener to the _quit button
            // Fill me in
            _quit.onClick.AddListener(OnQuitClick);
        }

        private static void UpdateScore ()
        {
            _scoreText.text = "Score: " + Score;
        }


        /// <summary>
        /// Add a value to the Score and update the text appropriately
        /// </summary>
        /// <param name="value">How much to add to the Score</param>
        public static void AddScore (int value) {
            Score = Math.Max(0, Score + value);
            UpdateScore();
        }

        /// <summary>
        /// Called when the Quit button is clicked. Exits the game.
        /// </summary>
        private static void OnQuitClick () {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
