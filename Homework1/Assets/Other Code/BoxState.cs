using System;
using UnityEngine;

namespace Assets.Other_Code
{
    public class BoxState : MonoBehaviour
    {
        private int _pointValue = -1;
        private float _touchPlatformStart;
        private bool _hasTouchedPlatform;
        private Rigidbody2D _rigidBody;

        /// <summary>
        /// The player has caught this object at some point.
        /// </summary>
        private bool CaughtByPlayer { get { return _pointValue > 0; } }

        /// <summary>
        /// Initialize component at the start of the game.
        /// </summary>
        internal void Start () {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Called when something touches this box.
        /// </summary>
        /// <param name="other">The collider2D of the object that hit us</param>
        internal void OnCollisionEnter2D (Collision2D other) {
            if (_hasTouchedPlatform) return; // we've already touched a platform

            if (other.gameObject.GetComponent<Platform>() != null) {
                TouchPlatform();
                Tutorial.UserAction(TutorialState.DepositBox);
            }

            if (CaughtByPlayer) return;

            if (other.gameObject.GetComponent<PlayerController>() != null) {
                _pointValue = 1;
                UpdateColor();
                Tutorial.UserAction(TutorialState.CatchBox);
            } else {
                var otherBox = other.gameObject.GetComponent<BoxState>();
                if (otherBox == null) return;

                if (otherBox._hasTouchedPlatform) {
                    TouchPlatform();
                } else {
                    // Collided with another box
                    _pointValue = Math.Max(_pointValue, otherBox._pointValue + 3);
                    UpdateColor();
                    Tutorial.UserAction(TutorialState.StackBox);
                }
            }
        }

        /// <summary>
        /// Called whenever we touch the platform.
        /// </summary>
        private void TouchPlatform () {
            _touchPlatformStart = Time.time;
            _hasTouchedPlatform = true;
            if (!CaughtByPlayer) { SetColor(Color.red); }
        }

        /// <summary>
        /// Check to see if we need to destroy the object or update its color.
        /// Called once per display frame.
        /// </summary>
        internal void Update () {
            if (_hasTouchedPlatform && _rigidBody.IsSleeping() && CaughtByPlayer) {
                UserInterface.AddScore(_pointValue);
                Destroy(gameObject);
            }

            if (_hasTouchedPlatform && (Time.time - _touchPlatformStart) > 60f) { Destroy(gameObject); }

            var y = transform.position.y;

            if (y < 0f && !CaughtByPlayer) { SetColor(Color.red); }

            if (y < -20f) {
                UserInterface.AddScore(-1);
                Destroy(gameObject);
            }
        }

        private void UpdateColor () {
            if (CaughtByPlayer) {
                SetColor(new Color(0, Mathf.Min(1, (_pointValue + 2) / 11f), 0, 1));
            }
        }

        private void SetColor (Color color) { GetComponent<SpriteRenderer>().color = color; }
    }
}
