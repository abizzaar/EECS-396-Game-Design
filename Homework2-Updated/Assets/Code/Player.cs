using System;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code
{
    /// <summary>
    /// Player controller class
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Player : MonoBehaviour, ISaveLoad
    {
        private static string _fireaxis;
        private Rigidbody2D _rb;
        private Gun _gun;

        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            _rb = GetComponent<Rigidbody2D>();
            _gun = GetComponent<Gun>();

            _fireaxis = Platform.GetFireAxis();
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            HandleInput();
        }

        /// <summary>
        /// Check the controller for player inputs and respond accordingly.
        /// </summary>
        private void HandleInput () {
			if (Input.GetAxis ("Horizontal") != 0) {
				Turn (Input.GetAxis ("Horizontal"));
			}
			if (Input.GetAxis ("Vertical") != 0) {
				Thrust (Input.GetAxis ("Vertical"));
			}
			if (Input.GetAxis (_fireaxis) == 1) {
				Fire ();
			}
        }

        private void Turn (float direction) {
            if (Mathf.Abs(direction) < 0.02f) { return; }
            _rb.AddTorque(direction * -0.05f);
        }

        private void Thrust (float intensity) {
            if (Mathf.Abs(intensity) < 0.02f) { return; }
            _rb.AddRelativeForce(Vector2.up * intensity);
        }

        private void Fire () {
            _gun.Fire();
        }

        #region saveload

        public GameData OnSave () {
			PlayerGameData playerGameData = new PlayerGameData ();
			Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
			playerGameData.Pos = rigidBody.position;
			playerGameData.Velocity = rigidBody.velocity;
			playerGameData.Rotation = rigidBody.rotation;
			playerGameData.AngularVelocity = rigidBody.angularVelocity;
			return playerGameData;
        }

		//You can set the rotation of the RigidBody2D of an object by calling MoveRotation.
	//	You can make a magic Quaternion object for a given rotation by calling Quaternion.Euler(0, 0,
		//	rotation).
		//	Important: the angularVelocity field of the RigidBody2D object has a bug. When you ask a
		//	RigidBody2D what its angular velocity is, it tells you in degrees per second. But when you set it, it assume you’re giving the answer in radians per second. So when you’re setting the angular velocity to the saved value from the save file, multiply it by Mathf.Deg2Rad. Welcome to software development in the real world
       
		public void OnLoad (GameData data) {
			PlayerGameData playerGameData = (PlayerGameData) data;
			Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
			rigidBody.position = playerGameData.Pos;
			rigidBody.velocity = playerGameData.Velocity;
			rigidBody.MoveRotation(playerGameData.Rotation);
			rigidBody.angularVelocity  = playerGameData.AngularVelocity * Mathf.Deg2Rad ;
        }
        
        #endregion
    }

    public class PlayerGameData : GameData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity; // reaed as DEGREES but stored as RADIANS; COME ON UNITY
    }
}
