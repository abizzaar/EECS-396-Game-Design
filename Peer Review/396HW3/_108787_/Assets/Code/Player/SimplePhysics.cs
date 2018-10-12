using UnityEngine;

namespace Assets.Code.Player
{
    /// <inheritdoc />
    /// <summary>
    /// Class for simulating the player's physics
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimplePhysics : MonoBehaviour
    {
        public static readonly Vector2 Gravity = new Vector2(0f, -9.8f);

        public static float TimeScale { get; private set; }
        public static void Pause () { TimeScale = 0f; }
        public static void Unpause () { TimeScale = 1f; }

        public Material DebugMaterial;

        public delegate void OnCollision (Collider2D other);
        public event OnCollision CollisionDown = other => { }; // fill it in with an empty one at first

        private Rigidbody2D _rb;
        private LayerMask _mask;
        private DebugHUD _hud;

        private Vector2 _velocity;

        internal void Start () {
            _rb = GetComponent<Rigidbody2D>();
            _mask = LayerMask.GetMask("Platforms");
            _velocity = Vector2.right;

            _hud = new DebugHUD(DebugMaterial);

            Unpause();
        }

        internal void FixedUpdate ()
        {
            _velocity = (_velocity + Gravity * Time.fixedDeltaTime) * TimeScale;
            ProcessCollision();
            _rb.position = _rb.position + _velocity * Time.fixedDeltaTime;
        }


        /// <summary>
        /// Called whenever our player hits anything. Handles collisions by adjusting velocity.
        /// We're working under the assumption that everything that we hit is square.
        /// </summary>
        private void ProcessCollision ()
        {
            RaycastHit2D RightCast = Physics2D.BoxCast(_rb.position, new Vector2(1, 0.5f), 0, new Vector2(1, 0), 0.1f, _mask, -Mathf.Infinity, Mathf.Infinity);
            RaycastHit2D DownCast = Physics2D.BoxCast(_rb.position, new Vector2(0.5f, 1), 0, new Vector2(0, -1), 0.1f, _mask, -Mathf.Infinity, Mathf.Infinity);
            if (RightCast)
            {
                _velocity.x = 0;
            }
            if (DownCast)
            {
                _velocity.y = Mathf.Max(0, _velocity.y);
                CollisionDown.Invoke(DownCast.collider);
            }
        }

        /// <summary>
        /// Increase _velocity by some value
        /// </summary>
        /// <param name="value">The amount by which to increase the velocity</param>
        public void AddVelocity (Vector2 value) { _velocity += value; }

        internal void OnGUI () {
            var val = _velocity.normalized * 50f; // 50 pixel long vector in the direction of _velocity
            _hud.DrawArrow(val);
            _hud.DrawMagnitude(_velocity.magnitude);
        }

        /// <summary>
        /// Helper class to draw lines, etc. on the screen
        /// </summary>
        private class DebugHUD
        {
            private static readonly Vector2 HUDCorner = new Vector2(20f, 200f);
            private static readonly Vector3 ArrowStart = HUDCorner + new Vector2(50f, 50f);

            private readonly Material _mat;
            public DebugHUD (Material mat) { _mat = mat; }

            public void DrawArrow (Vector3 value)
            {
                Vector3 ArrowEnd = ArrowStart + value;
                if (!_mat)
                {
                    Debug.LogError("Please Assign a material on the inspector");
                    return;
                }
                GL.PushMatrix();
                _mat.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                GL.Vertex3(ArrowStart.x, ArrowStart.y, ArrowStart.z);
                GL.Vertex3(ArrowEnd.x, ArrowEnd.y, ArrowEnd.z);
                GL.End();
                GL.Begin(GL.TRIANGLES);
                GL.Color(Color.red);
                GL.Vertex3(ArrowEnd.x - value.y * 0.25f, ArrowEnd.y + value.x * 0.25f, ArrowEnd.z);
                GL.Vertex3(ArrowEnd.x + value.y * 0.25f, ArrowEnd.y - value.x * 0.25f, ArrowEnd.z);
                GL.Vertex3(ArrowEnd.x + value.x * 0.25f, ArrowEnd.y + value.y * 0.25f, ArrowEnd.z);
                GL.End();
                GL.PopMatrix();

            }

            public void DrawMagnitude (float magnitude) {
                Vector3 LowerLeft = new Vector3(ArrowStart.x - 25f, ArrowStart.y, ArrowStart.z);
                Vector3 LowerRight = new Vector3(ArrowStart.x - 5f, ArrowStart.y, ArrowStart.z);
                Vector3 UpperLeft = new Vector3(ArrowStart.x - 25f, ArrowStart.y + 10f, ArrowStart.z);
                Vector3 UpperRight = new Vector3(ArrowStart.x - 5f, ArrowStart.y + 10f, ArrowStart.z);

                if (!_mat)
                {
                    Debug.LogError("Please Assign a material on the inspector");
                    return;
                }
                GL.PushMatrix();
                _mat.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.QUADS);
                GL.Color(Color.red);
                GL.Vertex3(LowerLeft.x, LowerLeft.y + magnitude, LowerLeft.z);
                GL.Vertex3(LowerRight.x, LowerRight.y + magnitude, LowerRight.z);
                GL.Vertex3(UpperLeft.x, UpperLeft.y + magnitude, UpperLeft.z);
                GL.Vertex3(UpperRight.x, UpperRight.y + magnitude, UpperRight.z);
                GL.End();
                GL.PopMatrix();
            }
        }
    }
}
