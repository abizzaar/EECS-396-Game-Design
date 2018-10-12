using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NewSpawner : MonoBehaviour
{

    private int normals;
    private int fasts;
    private int strongs;
    private int wavenum;
    
    private string[][] Waves = new string[9][];
    private Base pbase;
    private GameObject enemy;
    private Vector3 spawnpoint;
    //Enemy Prefabs
    public GameObject _normal;
    public GameObject _fast;
    public GameObject _strong;

    private float normalcooldown = 1f;
    private bool read;
    private int i = 0;

    //Wave Panel Info
    public GameObject PanelPrefab;
    public Text PanelText;
    private Transform PanelStart;

    private bool enemiesremaining;



    // Use this for initialization
    void Start ()
	{
        
        normals = 0;
	    fasts = 0;
	    strongs = 0;
	    wavenum = 0;
	    enemiesremaining = true;
        pbase = GameObject.Find("Base").GetComponent<Base>();
	    spawnpoint = transform.position;
	    read = false;


        Waves[0] = new string[5] {"n", "n", "n", "n", "n"};
        Waves[1] = new string[10] { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" };
        Waves[2] = new string[15] { "n", "n", "n", "n", "n", "f", "f", "f", "f", "f", "n", "n", "n", "n", "n"};
        Waves[3] = new string[12] { "n", "n", "n", "n", "n", "s", "s", "n", "n", "n", "n", "n" };
        Waves[4] = new string[24] { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n", "s", "s", "s", "s", "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" };
        Waves[5] = new string[24] { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n", "s", "s", "s", "s", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f"};
        Waves[6] = new string[11] { "n", "n", "n", "n", "n", "f", "f", "f", "f", "f", "s"};
        Waves[7] = new string[6] { "f", "f", "f", "f", "f" , "s"};
        Waves[8] = new string[6] { "s", "s", "s", "s", "s", "s"};
        

    }
	
	// Update is called once per frame
	void Update ()
	{
        if (wavenum == 9 && i == Waves[wavenum-1].Length && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (pbase.CurrentHealth != 0)
            {
                pbase.win();
            }
        }
	    if (wavenum <= Waves.Length-1)
	    {
	        if (!read)
	        {

	            Wavereader(Waves[wavenum]);
	            read = true;
	            PanelUpdate();
	            normalcooldown = 10f;
	            PanelPrefab.GetComponent<WavePanel>().wait = false;

	        }

	        if (normalcooldown <= 0)
	        {
	            Wavespawner(Waves[wavenum][i]);


	            if (i > Waves[wavenum].Length - 1)
	            {

	                
	                    
	                wavenum++;
	                if (wavenum < Waves.Length)
	                {
	                    i = 0;
	                }
	                read = false;
	                
	                normals = 0;
	                fasts = 0;
	                strongs = 0;



	            }
	        }


	        normalcooldown -= Time.deltaTime;
	    }
	    else
	    {
	        //this is for debug
	    }
	}


   


    void PanelUpdate()
    {
        PanelText.text = string.Format("Wave Number: {0} \n Normal: {1}  \n Fast: {2}  \n Strong: {3} ", wavenum + 1, normals, fasts, strongs);
        
    }

    void Wavereader(string[] array)
    {
        foreach (String s in array)
        {
            if (s.Equals("n"))
            {
                normals++;
            }
            else if (s.Equals("f"))
            {
                fasts++;
            }
            else if (s.Equals("s"))
            {
                strongs++;
            }
        }
    }

    void Wavespawner(string s)
    {
        
            if (s.Equals("n"))
            {
                SpawnNormal();
            
            }
            else if (s.Equals("f"))
            {
                SpawnFast();

            }
            else if (s.Equals("s"))
            {
                SpawnStrong();
            
            }
        
    }

    

    void SpawnNormal()
    {
        enemy = (GameObject)Instantiate(_normal, spawnpoint, transform.rotation);
        enemy.GetComponent<NavMeshAgent>().Warp(spawnpoint);
        enemy.GetComponent<Enemy>();
        normalcooldown = 1f;
        i++;
    }

    void SpawnFast()
    {
        enemy = (GameObject)Instantiate(_fast, spawnpoint, transform.rotation);
        enemy.GetComponent<NavMeshAgent>().Warp(spawnpoint);
        enemy.GetComponent<Enemy>();
        normalcooldown = 1f;
        i++;

    }

    void SpawnStrong()
    {
        enemy = (GameObject)Instantiate(_strong, spawnpoint, transform.rotation);
        enemy.GetComponent<NavMeshAgent>().Warp(spawnpoint);
        enemy.GetComponent<Enemy>();
        normalcooldown = 5f;
        i++;

    }

    

}
