using UnityEngine;
using UnityEngine.UI;
using System;

namespace Assets
{
    // how far along in the tutorial are we?
    public enum TutorialState
    {
        Blank,
        MoveMouse,
        CatchBox,
        DepositBox,
        StackBox,
        PressKeys,
        DoneWithTutorial
    };

    public class Tutorial : MonoBehaviour
    {
        // should we show the tutorial at all?
        public bool ShowTutorial = true;


        private static Text _tutorialText;
        private static TutorialState _tutorialState = TutorialState.Blank;

        // the different messages to display throughout the tutorial
        private static readonly string[] TutorialStrings =
        {
            "",
            "Use the mouse to move the paddle",
            "Catch a box with the paddle",
            "Drop the box on the orange platform to score points",
            "Stack the boxes on the paddle to make them worth more",
            "Use arrow keys or WASD to tilt and raise the platform",
            ""
        };

        internal void Start () {
            _tutorialText = GameObject.Find("Tutorial Text").GetComponent<Text>();
            UpdateText();
        }

        /// <summary>
        /// Called (internally or externally) whenever the Player does something that might be of interest to the tutorial.
        /// Changes state appropriately.
        /// </summary>
        /// <param name="action">The type of event that just happened</param>
        public static void UserAction (TutorialState action) {
            if (action != _tutorialState) return; // this action wasn't relevant at this particular time.

            _tutorialState++;
            UpdateText();
        }

        
        //
        // Fill this function in

        private static void UpdateText ()
        {
			if (_tutorialState == TutorialState.Blank) {
				_tutorialText.text = TutorialStrings [0];
			}
			if (_tutorialState == TutorialState.MoveMouse) {
				_tutorialText.text = TutorialStrings [1];
			}
			if (_tutorialState == TutorialState.CatchBox) {
				_tutorialText.text = TutorialStrings [2];
			}
			if (_tutorialState == TutorialState.DepositBox) {
				_tutorialText.text = TutorialStrings [3];
			}
			if (_tutorialState == TutorialState.StackBox) {
				_tutorialText.text = TutorialStrings [4];
			}
			if (_tutorialState == TutorialState.PressKeys) {
				_tutorialText.text = TutorialStrings [5];
			}
			if (_tutorialState == TutorialState.DoneWithTutorial) {
				_tutorialText.text = TutorialStrings [6];
			}
        }


        internal void Update () {
            CheckActions();
        }

        // last known mouse position (used to see if the mouse has moved)
        private Vector3 _oldMousePosition;

        /// <summary>
        /// Checks to see if interesting things are happening and updates the tutorial accordingly
        /// </summary>
        private void CheckActions () {
            if (!ShowTutorial) { return; }
            if (_tutorialState == TutorialState.DoneWithTutorial) { return; } // nothing to see here

            if (_tutorialState == TutorialState.MoveMouse && Input.mousePosition != _oldMousePosition) {
                UserAction(TutorialState.MoveMouse);
            }

            _oldMousePosition = Input.mousePosition;

            if (_tutorialState == TutorialState.PressKeys && Input.anyKeyDown) {
                UserAction(TutorialState.PressKeys);
            }

            // don't display anything for the first two seconds because the player won't see it anyway
            if (_tutorialState == TutorialState.Blank && Time.time > 2f) {
                UserAction(TutorialState.Blank);
            }
        }
    }
}
