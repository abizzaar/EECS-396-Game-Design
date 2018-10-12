using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Structure
{
    public class Bullet : MonoBehaviour
    {
        private Transform target;
        public float speed = .000000000001f;
        public int damage = 1;

        public void Seek(Transform _target)
        {
            target = _target;
        }



        // Update is called once per frame
        internal void Update()
        {
            if (target == null)
            {
                Die();
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceInFrame = speed * Time.deltaTime;


            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 5);
            //transform.Translate(dir.normalized * distanceInFrame, Space.World);

        }

        private void Die()
        {
            Destroy(gameObject);
        }

        internal void OnCollisionEnter(Collision coll)
        {
            Die(); //Always die on collision
        }
    }
}