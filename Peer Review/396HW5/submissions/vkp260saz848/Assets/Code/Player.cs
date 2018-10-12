using System;
using System.IO;
using System.Xml.Serialization;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code
{
    /// <summary>
    /// Player controller class
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Player : MonoBehaviour
    {
        private string _fireaxis;
        private Rigidbody2D _rb;
        private Gun _gun;
        private string _name;
        private string _temp;

        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            _rb = GetComponent<Rigidbody2D>();
            _gun = GetComponent<Gun>();
            _name = name;
            _fireaxis = Platform.GetFireAxis() + _name.Substring(_name.Length - 1);
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            HandleInput();
        }

        /// <summary>
        /// Check the controller for player inputs and respond accordingly.
        /// </summary>
        private void HandleInput () {
            if(_name=="Player1")
            {
                Turn(Input.GetAxis("Horizontal"));
                Thrust(Input.GetAxis("Vertical"));
                if (Input.GetAxis(_fireaxis) > 0)
                {
                    Fire();
                }
            }
            else
            {
                Turn(Input.GetAxis("Horizontal2"));
                Thrust(Input.GetAxis("Vertical2"));
                if (Input.GetAxis(_fireaxis) > 0)
                {
                    Fire();
                }
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
            _gun.Fire(name);
        }
    }
}
