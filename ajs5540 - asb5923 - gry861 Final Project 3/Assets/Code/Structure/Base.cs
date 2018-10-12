using System;
using System.Runtime.CompilerServices;
using Assets.Code.Structure;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Code.Structure
{
    /// <summary>
    /// Player home base class
    /// </summary>
    public class Base : MonoBehaviour
    {
        
        public int StartingHealth = 100;
        public int CurrentHealth;
        public Slider HealthSlider;
        private static Text _healthText;
        private Collider stronghold;
        public GameObject gameover;
        public GameObject winover;
        
        
        //private bool isDead;
        //private bool damaged;

        // Use this for initialization
        internal void Start()
        {
            
            stronghold = gameObject.GetComponent<CapsuleCollider>();
            _healthText = GetComponent<Text>();
            CurrentHealth = StartingHealth;
            
            //isDead = false;
            Update();
        }

        // Update is called once per frame
        private void Update()
        {
            
            
        }

        public void AlterHealth(int value)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth + value);
            //if (CurrentHealth == 0)
                //isDead = true;
            HealthSlider.value = CurrentHealth;
            /*
            if (isDead)
            {
              Death();  //Do some special function when dead.  
            }
            */
            //Damaged stuff, not imp.
            _healthText.text = string.Format("Base Health: {0}/100", CurrentHealth).PadLeft(4, '0');
            if (CurrentHealth <= 0)
            {
                gameOver();
            }
        }

        internal void gameOver()
        {
            Time.timeScale = 0;
            gameover.SetActive(true);
        }

        internal void win()
        {
            Time.timeScale = 0;
            winover.SetActive(true);
        }


        internal void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                AlterHealth(enemy.DamageValue);
                
            }
        }
    }
}