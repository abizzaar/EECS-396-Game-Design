using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Object[] enemies;
    private float respawn;
    private float respawn2;
    private Vector3 spawnpoint;
    private GameObject pbase;

    public float waveWaitTime = 5f;
    private float enemyWaitTime = 2f;
    public float waveNumber;
    public bool waveNumChanged = false;
    private bool waveRunning = false;
    public int numberOfEnemiesNormal;
    public int numberOfEnemiesFast;
    public int numberOfEnemiesStrong;
    private GameObject Panel;
    
    // UI CODE
    private GameObject Normal;
    private GameObject Quick;
    private GameObject Strong;
    private Vector3 initialNormalPos;
    private Vector3 initialStrongPos;
    private Vector3 initialQuickPos;

    // Use this for initialization
    void Start()
    {
        //Enemies go along the ground or wherever we put the base
        pbase = GameObject.Find("Base");
        spawnpoint = transform.position;
        StartCoroutine(CurrentWave());
        waveRunning = true;
        waveNumber = 1;
        Panel = GameObject.Find("Panel");
        
        // UI CODE
        Normal = GameObject.Find("Normal");
        Quick = GameObject.Find("Quick");
        Strong = GameObject.Find("Strong");
        initialNormalPos = Normal.GetComponent<RectTransform>().localPosition;
        initialStrongPos = Strong.GetComponent<RectTransform>().localPosition;
        initialQuickPos = Quick.GetComponent<RectTransform>().localPosition;
        waveNumChanged = GetComponent<Spawner>().waveNumChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > respawn2 + 1f)
        {
            waveNumChanged = false;
        }
        if (!waveRunning)
        {
            float spawntime = Time.time;
            if (spawntime > respawn + waveWaitTime)
            {
                StopCoroutine(CurrentWave());
                waveRunning = true;
                StartCoroutine(CurrentWave());
            }
            
            
        }
        
        // UI CODE
        int enemiesNormal = numberOfEnemiesNormal;
        int enemiesStrong = numberOfEnemiesStrong;
        int enemiesQuick = numberOfEnemiesFast;


        if (waveNumber > 1)
        {
            float inc = Time.deltaTime * 1000 / waveWaitTime;

            Normal.GetComponent<RectTransform>().localPosition = new Vector3
            (
                Normal.GetComponent<RectTransform>().localPosition.x + inc,
                Normal.GetComponent<RectTransform>().localPosition.y,
                Normal.GetComponent<RectTransform>().localPosition.z
            );
        }
    }

 

    IEnumerator CurrentWave()
    {
        numberOfEnemiesNormal = Random.Range(2, 5);
        numberOfEnemiesStrong = Random.Range(1, 3);
        numberOfEnemiesFast = Random.Range(1, 3);
        int totalEnemies = numberOfEnemiesFast + numberOfEnemiesNormal + numberOfEnemiesStrong;
        List<Object> enemiesInWave = new List<Object>();

        for (int i = 0; i < numberOfEnemiesNormal; i++)
        {
            enemiesInWave.Add(enemies[0]);
        }
        for (int i = 0; i < numberOfEnemiesFast; i++)
        {
            enemiesInWave.Add(enemies[1]);
        }
        for (int i = 0; i < numberOfEnemiesStrong; i++)
        {
            enemiesInWave.Add(enemies[2]);
        }

        int currSize = totalEnemies;
        
        while (currSize != 0)
        {
            int indexOfCurrEnemy = Random.Range(0, currSize);
            Object.Instantiate(enemiesInWave[indexOfCurrEnemy], spawnpoint, transform.rotation, gameObject.transform);
            enemiesInWave[indexOfCurrEnemy] = enemiesInWave[currSize - 1];
            currSize = currSize - 1;
            yield return new WaitForSeconds(enemyWaitTime);
        }
        waveRunning = false;
        respawn = Time.time;
        waveWaitTime = (Random.value * 2) + 5;
        waveNumber = waveNumber + 1;
        waveNumChanged = true;
        respawn2 = Time.time;
    }
}