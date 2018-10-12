using UnityEngine;
using Assets.Code;

namespace Code {
public class Tower : MonoBehaviour {

    private Vector3 _pos;
    private GameObject _closestEnemy;
    
    private static Object _bulletPrefab;
    private const float CooldownTime = 3f;
    private float _lastFire;
    
    
    public GameObject GetClosestEnemy()
    {
            Collider[] radiusObjects = Physics.OverlapSphere(_pos, 50.0f);
            return FindClosestEnemy(_pos, radiusObjects);
    }

    // Use this for initialization
    void Start () {
        _pos = GetComponent<Transform>().position;
        _closestEnemy = null;
        
        _bulletPrefab = Resources.Load("Prefabs/Bullet");
        _lastFire = Time.time;
    }
    
    // Update is called once per frame
    void Update () {
        _pos = GetComponent<Transform>().position;
        
        //search for closest enetmy in radius = 50
        Collider[] radiusObjects= Physics.OverlapSphere(_pos, 50.0f);
        _closestEnemy = FindClosestEnemy(_pos, radiusObjects);
        if (_closestEnemy != null)
        {
            Vector3 relativePos = _pos - GetClosestEnemy().GetComponent<Transform>().position;
            transform.rotation = Quaternion.LookRotation(relativePos);

            if (ReadyToFire())
            {
                Fire();
            }
            
        }
    }

    private GameObject FindClosestEnemy(Vector3 towerPosition, Collider[] radiusObjects)
    {
        GameObject closestEnemy_ret = null;
        float closestDistance = Mathf.Infinity;
        float dist;

        for (int i=0; i<radiusObjects.Length; i++)
        {
            if (radiusObjects[i].gameObject.GetComponent<Enemy>() != null)
            {
                Vector3 directinoToEnemy = radiusObjects[i].gameObject.GetComponent<Transform>().position - towerPosition;
                dist = directinoToEnemy.sqrMagnitude;
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestEnemy_ret = radiusObjects[i].gameObject;
                }
            }    
        }

        return closestEnemy_ret;
    }
    
    public void Fire()
    {
        
        var enemy = transform.GetComponent<Tower>().GetClosestEnemy();
        if (enemy == null) return;
        
        var enemyPos = enemy.GetComponent<Transform>().position;
        var directionVector = (enemyPos - transform.position).normalized;

                
        var newBullet = (GameObject)Object.Instantiate(_bulletPrefab, transform.position + (3 * directionVector), Quaternion.identity);
        newBullet.GetComponent<Bullet>().Initialize(newBullet,  directionVector);
    }

    private bool ReadyToFire()
    {
        if ((Time.time - _lastFire) >= CooldownTime)
        {
            _lastFire = Time.time;
            return true;
        }
        return false;
    }
}
}

