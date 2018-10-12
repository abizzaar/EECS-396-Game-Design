using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private Rigidbody _rb;
        private int _health;
        public GameObject Base;
        private Vector3 _basePosition;
        private Vector3 _myPosition;
        public int Attack; //Leave this public for now so I can play with it/test it. You're also more than welcome to change the value in the inspector
        public int Speed;

        public void Initialize(int health)
        {
            _health = health;
        }

        // Use this for initialization
        void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _basePosition = Base.transform.position;
            _myPosition = gameObject.transform.position;
            _health = 50;
        }

        // Update is called once per frame
        void Update()
        {
            _rb.velocity = new Vector3(_basePosition.x - _myPosition.x, 0, _basePosition.z - _myPosition.z);
            _rb.velocity = _rb.velocity.normalized * Speed;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Die();
            }

            if (_health > 0)
            {
                StartCoroutine(Flash());
            }
        }

        public void Die()
        {
            //Add explosion/death animation? #visualfeedback
            Destroy(gameObject);
        }

        //Because it's good game design #visualfeedback
        IEnumerator Flash()
        {
            Material myMaterial = gameObject.GetComponent<MeshRenderer>().material;

            myMaterial.color = Color.gray; //Too tired, please pick a better color
            yield return new WaitForSeconds(0.1f);
            myMaterial.color = Color.red;
        }
    }
}