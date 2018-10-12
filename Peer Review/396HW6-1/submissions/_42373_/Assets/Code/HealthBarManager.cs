using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{

    private Slider healthBar;
    private int currentHealth;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    
    // Use this for initialization
    void Start ()
    {
        currentHealth = 100;
    }
    

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
    }

    public void setHealth(int points)
    {
        currentHealth = points;
    }

}
