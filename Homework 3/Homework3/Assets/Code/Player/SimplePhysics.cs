using UnityEditor;
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
            _velocity = _velocity + (TimeScale * Gravity * Time.fixedDeltaTime);
            ProcessCollision();
            _rb.position = _rb.position + (TimeScale * _velocity * Time.fixedDeltaTime);
        }


        /// <summary>
        /// Called whenever our player hits anything. Handles collisions by adjusting velocity.
        /// We're working under the assumption that everything that we hit is square.
        /// </summary>
        private void ProcessCollision () {
          //  
           // Player
            BoxCollider2D playerBox  = GameObject.Find("Player(Clone)").GetComponent<BoxCollider2D>();
            Vector2 vectorDown = new Vector2(0, -1);
            Vector2 vectorRight = new Vector2(1, 0);       
            
            // casting down
            Vector2 centerBoxCast = new Vector2(_rb.position.x + (playerBox.size.x / 2), _rb.position.y);
            Vector2 sizeBoxCast = new Vector2(playerBox.size.x / 2, playerBox.size.y  / 2);
            
            RaycastHit2D downRayCast = Physics2D.BoxCast(centerBoxCast, sizeBoxCast, 0, vectorDown, playerBox.size.x / 2, _mask);
            
            // casting right
            RaycastHit2D rightRayCast = Physics2D.BoxCast(centerBoxCast, sizeBoxCast, 0, vectorRight, playerBox.size.x / 2, _mask);

            if (downRayCast.collider != null) {

                if (_velocity.y <= 0)
                {
                    _velocity.y = 0;
                    CollisionDown.Invoke(downRayCast.collider);
                }
            }         
            if (rightRayCast.collider != null)
            {
                _velocity.x = 0;
            }
            
            
            
        }

        /// <summary>
        /// Increase _velocity by some value
        /// </summary>
        /// <param name="value">The amount by which to increase the velocity</param>
        public void AddVelocity (Vector2 value) { _velocity += TimeScale * value; }

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

            public void DrawArrow (Vector3 value) {

                GL.PushMatrix();
                _mat.SetPass(0);
                GL.LoadPixelMatrix();
                
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                GL.Vertex(ArrowStart);
                Vector3 lineEndPoint = (value * 2) + ArrowStart;
                GL.Vertex(lineEndPoint);
                GL.End();
                
                GL.Begin(GL.TRIANGLES);
                GL.Color(Color.red);
                Vector3 perp_vector = new Vector2(-value.y, value.x);
                GL.Vertex(lineEndPoint + perp_vector.normalized * 10f);
                GL.Vertex(lineEndPoint + perp_vector.normalized * -10f);
                GL.Vertex((value.normalized * 120f) + ArrowStart);
                GL.End();
                
                GL.PopMatrix();
            }

            public void DrawMagnitude (float magnitude) {
                GL.PushMatrix();
                _mat.SetPass(0);
                GL.LoadPixelMatrix();
                Vector2 leftBottomV = new Vector2(50f, 50f);
                Vector2 rightBottomV = new Vector2(60f, 50f);
                
                GL.Begin(GL.QUADS);
                GL.Color(Color.red);               
                GL.Vertex(leftBottomV);
                GL.Vertex(rightBottomV);
                GL.Vertex(new Vector2(rightBottomV.x, rightBottomV.y + magnitude * 10));
                GL.Vertex(new Vector2(leftBottomV.x, leftBottomV.y + magnitude * 10));                
                GL.End();
                
                GL.Begin(GL.LINES);               
                GL.Vertex(leftBottomV);
                GL.Vertex(new Vector2(leftBottomV.x, leftBottomV.y * 10));        
                GL.End();
                
                GL.Begin(GL.LINES);              
                GL.Vertex(leftBottomV);
                GL.Vertex(rightBottomV);
                GL.End();
                
                GL.PopMatrix();
            }

           
        }
    }
}
