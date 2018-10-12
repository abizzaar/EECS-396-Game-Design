using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code.Structure
{
    public class NormalTower : MonoBehaviour
    {
        public GameObject target;

        public float sphereRadius;
        public LayerMask layerMask;
        public float range = 2.5f; //maxDistance = range

        private Vector3 origin;
        private Vector3 direction;

        private float currentHitDistance;

        public string enemyTag = "Enemy";

        //private Gun _gun;
        public float turnSpeed = 10f;
        public float fireRate = 1f;
        private float fireCooldown = 0f;

        /// <summary>
        /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
        /// </summary>
        public Object _bullet;
        public Transform firePoint;


        // Use this for initialization
        internal void Start()
        {
            //_gun = GetComponent<Gun>();
            //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        //Only check for nearest enemy every fixed interval, as to avoid overly high computational costs of every frame calculation. 2/second (Optional)
        internal void UpdateTarget()
        {
            origin = transform.position;
            direction = transform.forward;
            RaycastHit hit;
            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, range, layerMask,
                QueryTriggerInteraction.UseGlobal))
            {
                target = hit.transform.gameObject;
                currentHitDistance = hit.distance;
            }
            else
            {
                target = null;
                currentHitDistance = range;
            }
            //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            /*foreach (GameObject enemy in enemies)
            {
                float 
            }
            */
        }

        // Update is called once per frame
        // Detects nearest enemy (starts firing at regular basis), rotates towards enemy.
        
        /*Checklist
         * 1. First check proximity for enemy using sphere cast.
         * 2. If there is an enemy do two things:
         * 2a. Start rotating towards the enemy
         * 2b. Start firing bullets at a regular interval
         * 3. Only change targets once the first enemy has disappeared or exited the detection range.
         */

        void Update()
        {
            UpdateTarget();
            if (target == null)
                return;
                

            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            if (fireCooldown <= 0)
            {
                Fire();
                fireCooldown = 1f / fireRate;
            }
            fireCooldown -= Time.deltaTime;
        }

        void Fire()
        {
            GameObject bull = (GameObject)Instantiate(_bullet, firePoint.position, firePoint.rotation);
            Bullet bullE = bull.GetComponent<Bullet>();
            if (bullE != null)
            {
                bullE.Seek(target.transform);
            }
        }


        //Shows range of the turret/SphereCast.
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
            Debug.DrawLine(origin, origin + direction * currentHitDistance);
            Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
        }

       

    }
}