using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTower : Tower {

    // Use this for initialization
    void Start () {
        towerCooldown = 1.0f;
        damage = 1;
        range = 3;
        bullet = Resources.Load("SimpleBullet");
    }
	
	// Update is called once per frame
	void Update () {
        // Look at closest Enemy
        /*
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, range, transform.forward, out hit, 10))
        {
            Vector3 relativePos = hit.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }*/

        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d < closestDistance)
            {
                closestDistance = d;
                closestEnemy = e;
            }
        }

        towerTimer -= Time.deltaTime;
        if (closestEnemy)
        {
            Vector3 forward = new Vector3(closestEnemy.transform.position.x - transform.position.x, 0.0f, closestEnemy.transform.position.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(forward);
            transform.rotation = rotation;
            if (towerTimer <= 0.0f)
            {
                Shoot(closestEnemy);
                towerTimer = towerCooldown;
            }
        }
	}
    
    public override void Shoot(Enemy enemy)
    {
        Transform towerAim = transform.Find("Aim").transform;
        GameObject newBullet = (GameObject)Object.Instantiate(bullet, towerAim.position, towerAim.rotation);
        newBullet.transform.SetParent(transform);
        newBullet.GetComponent<Bullet>().Initialize(enemy.transform.position, Time.time);
    }
}
