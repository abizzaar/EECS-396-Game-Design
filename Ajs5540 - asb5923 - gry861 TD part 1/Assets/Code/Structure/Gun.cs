using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Code.Structure
{
    /*
    public class Gun : MonoBehaviour
    {
        public float speed = 10f;

        public void Fire(Transform target)
        {
            if (target == null)
            {
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            Fire(target.transform);

            Game.Bullets.ForceSpawn(transform.position, lookRotation, speed * dir, Time.time + Bullet.Lifetime);
        }
    }
    */
}
