using UnityEngine;

namespace Assets
{
    /// <inheritdoc />
    /// <summary>
    /// Input handler for the Player's paddle.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        // miscellaneous constants. feel free to tweak
        private const float TiltAngle = 45f;

        private const float JumpHeight = 1f;
        private const float MouseStiffness = 10f;
        private const float VerticalStiffness = 10f;
        private const float TorsionalStiffness = 10f;

        // store the player's RigidBody2D so that we don't have to constantly access it
        private Rigidbody2D _rigidBody;

        internal void Start () {
            _rigidBody = GetComponent<Rigidbody2D>();
        }


        // Called once per Physics Update loop
        internal void FixedUpdate () {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPosition = transform.position;

            var mouseOffset = mousePosition - playerPosition;
            mouseOffset.y = 0;

            _rigidBody.AddForce(MouseStiffness * mouseOffset);
            _rigidBody.AddForce(new Vector2(
                0,
                VerticalStiffness * (VerticalSetPoint - transform.position.y)));

            _rigidBody.AddTorque(TorsionalStiffness * (RotationalSetPoint - _rigidBody.rotation));
        }


        private static float RotationalSetPoint {
            get { return TiltLeft ? (TiltAngle) : TiltRight ? -TiltAngle : 0; }
        }

        private static float VerticalSetPoint {
            get { return PushUp ? JumpHeight : PushDown ? (-JumpHeight) : 0; }
        }


        //
        // Fill in these functions

        /// <summary>
        /// Whether the D or Right Arrow Key is currently being pressed.
        /// Tilts the paddle to the right.
        /// </summary>
        private static bool TiltRight {
            get { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow); }
        }

        /// <summary>
        /// Whether the A or Left Arrow Key is currently being pressed.
        /// Tilts the paddle to the left.
        /// </summary>
        private static bool TiltLeft {
            get { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow); }
        }

        /// <summary>
        /// Whether the S or Down Arrow Key is currently being pressed.
        /// Pushes the paddle down.
        /// </summary>
        private static bool PushDown {
            get { return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow); }
        }

        /// <summary>
        /// Whether the W or Up Arrow Key is currently being pressed.
        /// Pushes the paddle up.
        /// </summary>
        private static bool PushUp {
            get { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); }
        }
    }
}
