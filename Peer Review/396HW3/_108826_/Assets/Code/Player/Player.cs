using UnityEngine;

namespace Assets.Code.Player
{

    public class Player : MonoBehaviour
    {
        private static readonly Vector2 Acceleration = new Vector2(2f, 0f);
        private static readonly Vector2 JumpVelocity = new Vector2(0f, 10f);

        private SimplePhysics _physics;
        private bool _jumping; // are we jumping?

        internal void Start () {
            _physics = GetComponent<SimplePhysics>();
            _physics.CollisionDown += CollisionDown;

        }

        internal void Update () {
            CheckKeys();
            if (!_jumping) { _physics.AddVelocity(Acceleration * Time.deltaTime); } // accelerate when we're on the ground
        }

        private void CheckKeys () {
            if (Input.GetKeyDown(KeyCode.P)) { Game.Ctx.Clock.TogglePause(); }
            if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        }

        private void CollisionDown (Collider2D other) {
            var platform = other.gameObject.GetComponent<Platform>();
            if (platform == null) { return; } // shouldn't happen ;)

            if (!platform.LandedOn) {
                platform.Land();
                // score stuff
            }

            _jumping = false;
        }

        private void Jump () {
            if (!_jumping) { _physics.AddVelocity(JumpVelocity); }
            _jumping = true;
        }
    }
}