using UnityEngine;

namespace Code
{
    public class Bullet : MonoBehaviour
    {
        public const float Lifetime = 20f; // bullets last this long
        public const int Damage = 20;
        public const float SpeedScalar = 5f;
        
        private float _deathtime;
        private Enemy _target;
        private Vector3 _targetVelocity;
        private Rigidbody _rb;

        public void Initialize (Vector3 bulletVelocity, Enemy target, float deathtime)
        {
            _targetVelocity = target.gameObject.GetComponent<Rigidbody>().velocity; 
            _target = target;
            _rb = GetComponent<Rigidbody>();
            _rb.velocity = SpeedScalar * bulletVelocity + _targetVelocity;
            _rb.useGravity = false;
            _deathtime = Time.time + deathtime;
            
        }

        internal void Update () {
            if (Time.time > _deathtime || !_target)
            {
                Die();
                return;
            }
            
            if (_targetVelocity != _target.gameObject.GetComponent<Rigidbody>().velocity)
            {
                UpdateRotationAndVelocity();
            }
        }

        private void UpdateRotationAndVelocity()
        {
            Vector3 currentTargetVelocity = _target.gameObject.GetComponent<Rigidbody>().velocity;
            _rb.velocity -= _targetVelocity;
            _rb.velocity += currentTargetVelocity;
            _targetVelocity = currentTargetVelocity;

            Quaternion newFacing = Quaternion.LookRotation(_target.transform.position - transform.position);
            newFacing *= Quaternion.Euler(90, 0, 0);
            transform.rotation = newFacing;
        }

        internal void OnCollisionEnter (Collision other) {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Die();
            }
        }

        private void Die () {
            Destroy(gameObject);
        }
    }
}