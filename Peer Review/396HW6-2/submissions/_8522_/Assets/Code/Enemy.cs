using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    protected int life { get; set; }
    protected float speed { get; set; }
    protected int damage { get; set; }
    protected Base homeBase { get; set; }
    protected Transform healthBar { get; set; }

/*    internal void FixedUpdate()
    {
        float velocity = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(homeBase.transform.position.x, transform.position.y, homeBase.transform.position.z), velocity);
    }*/

    public void showHealthBar()
    {
        float scaleFactor = 0.2f;
        healthBar.localScale = new Vector3(life * 1.0f * scaleFactor, healthBar.localScale.y, healthBar.localScale.z);
    }

    public void ReceiveDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            Globals.pm.AddMoney(500);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Damage Base
        if (other.GetComponent<Base>())
        {
            homeBase.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
