using System;
using Assets.Code.Structure;
using UnityEditor.IMGUI.Controls;
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
            // TODO fill me in
            Turn(Input.GetAxis("Horizontal"));
            Thrust(Input.GetAxis("Vertical"));
            if (Input.GetAxis(_fireaxis) == 1)
                Fire();
            
            
            



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

        // TODO fill me in
        public GameData OnSave () {
            PlayerGameData Playerdata = new PlayerGameData();
            Playerdata.Pos = _rb.position;
            Playerdata.Velocity = _rb.velocity;
            Playerdata.AngularVelocity = _rb.angularVelocity;
            Playerdata.Rotation = _rb.rotation;
            return Playerdata;
        }

        // TODO fill me in
        public void OnLoad (GameData data)
        {
            PlayerGameData player1 = (PlayerGameData) data;
            _rb.MovePosition(player1.Pos);
            _rb.velocity = player1.Velocity;
            _rb.MoveRotation(player1.Rotation);
            _rb.angularVelocity = player1.AngularVelocity * Mathf.Deg2Rad;

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
