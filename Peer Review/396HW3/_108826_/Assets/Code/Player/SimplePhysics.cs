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
		public static void Pause() { TimeScale = 0f; }
		public static void Unpause() { TimeScale = 1f; }

		public Material DebugMaterial;

		public delegate void OnCollision(Collider2D other);
		public event OnCollision CollisionDown = other => { }; // fill it in with an empty one at first

		private Rigidbody2D _rb;
		private LayerMask _mask;
		private DebugHUD _hud;

		private Vector2 _velocity;

		internal void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_mask = LayerMask.GetMask("Platforms");
			_velocity = Vector2.right;

			_hud = new DebugHUD(DebugMaterial);

			Unpause();
		}

		internal void FixedUpdate()
		{
			// DONE
			_velocity.x = _velocity.x + Gravity.x * (TimeScale / 100);
			_velocity.y = _velocity.y + Gravity.y * (TimeScale / 100);
			ProcessCollision();
			_rb.position = new Vector2(_rb.position.x + _velocity.x * (TimeScale / 100), _rb.position.y + _velocity.y * (TimeScale / 100));
		}


		/// <summary>
		/// Called whenever our player hits anything. Handles collisions by adjusting velocity.
		/// We're working under the assumption that everything that we hit is square.
		/// </summary>
		private void ProcessCollision()
		{
			// DONE
			Vector2 sizeX = GetComponent<BoxCollider2D>().size;
			sizeX.x *= 0.7f;
			Vector2 sizeY = GetComponent<BoxCollider2D>().size;
			sizeY.y *= 0.7f;

			RaycastHit2D collision_x = Physics2D.BoxCast(_rb.position, sizeY, 0, new Vector2(1, 0), 1, _mask);
			RaycastHit2D collision_y = Physics2D.BoxCast(_rb.position, sizeX, 0, new Vector2(0, -1), 1, _mask);
			if (collision_y.collider != null)
			{
				if (_velocity.y < 0)
				{
					_velocity.y = 0;
				}
				CollisionDown.Invoke(collision_y.collider);
			}

			if (collision_x.collider != null)
			{
				_velocity.x = 0.1f;
				CollisionDown.Invoke(collision_x.collider);
			}
		}

		/// <summary>
		/// Increase _velocity by some value
		/// </summary>
		/// <param name="value">The amount by which to increase the velocity</param>
		public void AddVelocity(Vector2 value) { _velocity += value; }

		internal void OnGUI()
		{
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
			public DebugHUD(Material mat) { _mat = mat; }

			public void DrawArrow(Vector3 value)
			{
				// DONE
				GL.PushMatrix();
				_mat.SetPass(0);
				GL.LoadPixelMatrix();
				GL.Begin(GL.LINES);
				GL.Color(Color.red);
				GL.Vertex(ArrowStart);
				GL.Vertex(ArrowStart + value);
				GL.End();
				GL.PopMatrix();

				GL.PushMatrix();
				_mat.SetPass(0);
				GL.LoadPixelMatrix();
				GL.Begin(GL.TRIANGLES);
				GL.Color(Color.red);
				GL.Vertex3(ArrowStart.x + value.x, ArrowStart.y + value.y, ArrowStart.z + value.z);
				GL.Vertex3(ArrowStart.x + value.x - value.x / 8 + value.y / 12, ArrowStart.y + value.y - value.y / 8 - value.x / 12, ArrowStart.z + value.z - value.z / 8);
				GL.Vertex3(ArrowStart.x + value.x - value.x / 8 - value.y / 12, ArrowStart.y + value.y - value.y / 8 + value.x / 12, ArrowStart.z + value.z - value.z / 8);
				GL.End();
				GL.PopMatrix();
			}

			public void DrawMagnitude(float magnitude)
			{
				// DONE
				GL.PushMatrix();
				_mat.SetPass(0);
				GL.LoadPixelMatrix();
				GL.Begin(GL.QUADS);
				GL.Color(Color.red);
				GL.Vertex3(10f, 10f, 0);
                GL.Vertex3(10f, 10f + magnitude * 10 * TimeScale, 0);
				GL.Vertex3(30f, 10f + magnitude * 10 * TimeScale, 0);
				GL.Vertex3(30f, 10f, 0);
				GL.End();
				GL.PopMatrix();
			}
		}
	}
}
