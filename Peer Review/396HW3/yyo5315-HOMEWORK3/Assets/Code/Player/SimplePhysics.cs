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

        internal void FixedUpdate () {
            // TODO fill me in
            ProcessCollision();
            _velocity = _velocity + Gravity * Time.deltaTime * TimeScale;
            _rb.position = _rb.position + _velocity * Time.deltaTime * TimeScale;


        }


        /// <summary>
        /// Called whenever our player hits anything. Handles collisions by adjusting velocity.
        /// We're working under the assumption that everything that we hit is square.
        /// </summary>
        private void ProcessCollision () {
            // TODO fill me in
          
            Vector2 Box_size = new Vector2(GetComponent<BoxCollider2D>().size.x/3, GetComponent<BoxCollider2D>().size.y);
            double distance_y = _velocity.y * Time.deltaTime * TimeScale;
			RaycastHit2D raycast_hit_y=Physics2D.BoxCast(_rb.position,Box_size,0,new Vector2(0,-1), (float)distance_y, _mask,Mathf.Infinity);
            if (raycast_hit_y.collider!=null)
            {
                _velocity.y = Mathf.Max(_velocity.y, 0f);
                CollisionDown.Invoke(raycast_hit_y.collider);
            } 
            Box_size = new Vector2(GetComponent<BoxCollider2D>().size.x , GetComponent<BoxCollider2D>().size.y /3);
            double distance_x = _velocity.x * Time.deltaTime * TimeScale;
            RaycastHit2D raycast_hit_x = Physics2D.BoxCast(_rb.position, Box_size, 0, new Vector2(1, 0), (float)distance_x, _mask, Mathf.Infinity);
            if (raycast_hit_x.collider != null)
                _velocity.x = 0f;
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

            public void DrawArrow (Vector3 value) {
				// TODO fill me in
				GL.PushMatrix();
                _mat.color= Color.red;
                _mat.SetPass(0);
				GL.LoadPixelMatrix();
                GL.Begin(GL.LINES);
                GL.Vertex(ArrowStart);
                GL.Vertex(ArrowStart +value);
				GL.End();
                GL.PopMatrix();

				GL.PushMatrix();
                _mat.color = Color.red;
                _mat.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.TRIANGLES);
                Vector3 prop = new Vector3(-value.y, value.x,value.z);
                GL.Vertex(ArrowStart + value + prop/4 );
                GL.Vertex(ArrowStart + value - prop/4 );
                GL.Vertex(ArrowStart + value + value/5);
                GL.End();

				GL.PopMatrix();
            }

            public void DrawMagnitude (float magnitude) {
				// TODO fill me in

				GL.PushMatrix();
				_mat.SetPass(0);
                _mat.color = Color.red;
                GL.LoadPixelMatrix();
				GL.Begin(GL.QUADS);;
                GL.Vertex(HUDCorner);
                GL.Vertex(HUDCorner+new Vector2(0,magnitude));
                GL.Vertex(HUDCorner + new Vector2(0, magnitude)+new Vector2(10f,0));
				GL.Vertex(HUDCorner+new Vector2(10f, 0));
				GL.End();
				_mat.color = Color.white;
				_mat.SetPass(0);
				GL.Begin(GL.LINES);
                GL.Vertex(HUDCorner);
                GL.Vertex(HUDCorner+new Vector2(0,170));
				GL.End();
                GL.PopMatrix();

            }
        }
    }
}
