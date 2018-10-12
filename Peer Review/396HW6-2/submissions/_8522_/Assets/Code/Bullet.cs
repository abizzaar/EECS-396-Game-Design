using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {
    public int damage { get; set; }
    public float speed { get; set; }
    public float lifeTime { get; set; }
    public float deathTime { get; set; }
    public Vector3 shooting_dir { get; set; }

    public abstract void Attack(Enemy enemy);
    public abstract void Initialize(Vector3 trgt_pos, float spawnTime);

    internal void FixedUpdate()
    {
        float velocity = speed * Time.deltaTime;
        transform.position += shooting_dir * velocity;
        //transform.rotation = Quaternion.LookRotation(shooting_dir);
    }

    void Update()
    {
        if (Time.time > deathTime) { Die(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Receive Damage from tower
        if (other.GetComponent<Enemy>())
        {
            // Do something to enemy (right now just giving damage)
            Attack(other.GetComponent<Enemy>());
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
