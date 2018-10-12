using UnityEngine;

namespace Code
{
    public class Enemy : MonoBehaviour
    {
        private Rigidbody rb;
        private int damage = 15;
        private int health = 20;
        
        public void Initialize () {
            
            rb = GetComponent<Rigidbody>();

            rb.velocity = Vector3.back;
            rb.useGravity = false; 

        }

        internal void Update()
        {
            Vector3 bottomLeft = Game.Grid.getCell(0, 0).transform.position;
            
            if (rb.position.z <= bottomLeft.z)
            {
                rb.velocity = Vector3.right;
            }
        }


        internal void OnCollisionEnter (Collision other)
        {
            if (other.gameObject.GetComponent<Base>())
            {
                Destroy(gameObject);
                Game.Health.doDamage(damage);
            }
            else if (other.gameObject.GetComponent<Bullet>())
            {
                health -= Bullet.Damage;
                if (health < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}