using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour {
    protected float towerCooldown { get; set; }
    protected float towerTimer { get; set; }
    protected int damage { get; set; }
    protected int range { get; set; }
    protected Object bullet { get; set; }

    public abstract void Shoot(Enemy enemy);


}
