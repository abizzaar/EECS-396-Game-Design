using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet {
    public override void Initialize(Vector3 trgt_pos, float _spawnTime)
    {
        damage = 2;
        speed = 12.0f;
        lifeTime = 1.0f;
        deathTime = _spawnTime + lifeTime;
        shooting_dir = (trgt_pos - transform.position).normalized;
    }

    public override void Attack(Enemy enemy)
    {
        enemy.ReceiveDamage(damage);
    }
}
