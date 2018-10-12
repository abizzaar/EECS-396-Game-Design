using UnityEngine;

namespace Code
{
    public class Tower : MonoBehaviour
    {
        public Enemy previousClosest = null;
        private const float SpawnTime = .45f;
        private float _lastSpawn;
      
        
        public void Initialize ()
        {
            _lastSpawn = -SpawnTime;
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update ()
        {

            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Enemy closest = null;
            float closestDistance = Mathf.Infinity;
            
                
            foreach (Enemy x in enemies)
            {
                float currDistance = Vector3.Distance(transform.position, x.transform.position);
                if (currDistance < closestDistance)
                {
                    closest = x;
                    closestDistance = currDistance;
                }
            }
            
            if (closest)
            {
                if (previousClosest)
                {
                    previousClosest.GetComponent<MeshRenderer>().material.color = Color.white;
                }
                closest.GetComponent<MeshRenderer>().material.color = Color.red;
                Vector3 relativePos = closest.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                rotation *= Quaternion.Euler(0, 90, 0);
                transform.rotation = (rotation);
                previousClosest = closest;
            }
            
            if ((Time.time - _lastSpawn) < SpawnTime || !previousClosest) return;
            _lastSpawn = Time.time;
            Shoot();
    
        }


        private void Shoot()
        {
            GameObject pointer = GameObject.Find("TowerSmallCylinder");

            Vector3 facingDirection = previousClosest.transform.position - transform.position;
            facingDirection.Scale(pointer.transform.localScale);    //This needs to be fixed but I don't understand
            //facingDirection += new Vector3(.3f, 0, 0);                //unity enuf right now to make this better
            facingDirection *= 0.5f;
            
            Vector3 barrelCenter = /*transform.position + */pointer.transform.position;
            Vector3 barrelEnd = barrelCenter + facingDirection;
            Vector3 directionToEnemy = previousClosest.transform.position - barrelEnd;

            Quaternion rotationToEnemy = Quaternion.LookRotation(directionToEnemy);
            rotationToEnemy *= Quaternion.Euler(90, 0, 0);
            
            Game.Bullets.ForceSpawn(barrelEnd, rotationToEnemy, directionToEnemy, previousClosest);
            
            
        }
    }
}