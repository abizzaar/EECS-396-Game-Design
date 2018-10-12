using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    //Time to first spawn
    private float countdown;

    //Time between waves
    public float waveCDTime = 10f;

    //WaveInfo
    public int waveNumber;

    public float difficultyMultiplier = 1.05f; //Multiplier of HP/Damage/Gold per wave
    public int maxWave; //Maximum number of waves

    //How many enemies to add per new wave
    private int alterNormal;
    private int alterFast;
    private int alterStrong;

    //SpawnInfo
    private Vector3 spawnPoint;

    private GameObject pbase;

    //Enemy Prefabs
    public GameObject normal;
    public GameObject fast;
    public GameObject strong;
    
    //Wave Panel Info
    public GameObject PanelPrefab;
    public Text PanelText;
    private Transform PanelStart;

    //IDK
    private Transform target;

    void Start()
    {
        countdown = 10f;
        waveNumber = 1;
        alterNormal = 3;
        alterFast = 2;
        alterStrong = 1;

        spawnPoint = transform.position;
        pbase = GameObject.Find("Base");
        PanelStart = PanelPrefab.transform;
        target = GameObject.Find("Base").transform;

        PanelText.text = string.Format("Wave Number: {0} \n Normal: {1}  \n Fast: {2}  \n Strong: {3} ", waveNumber, alterNormal, alterFast, alterStrong);
        //StartCoroutine(BeginWave(waveNumber));
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            if (waveNumber <= maxWave)
            {
                StartCoroutine(SpawnWave(waveNumber));
                countdown = 10f;
            }
        }
        if (waveNumber >= maxWave && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            pbase.GetComponent<Base>().win();
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave(int waveN)
    {
        int spawnN = alterNormal * waveN;
        int spawnF = alterFast * waveN;
        int spawnS = alterStrong * waveN;

        if(waveNumber<maxWave)
            PanelText.text = string.Format("Wave Number: {0} \n Normal: {1}  \n Fast: {2}  \n Strong: {3} ", waveNumber+1, spawnN+3, spawnF+2, spawnS+1);
        if(waveNumber == maxWave)
            PanelText.text = string.Format("Beat all enemies and win!");

        for (int i = 0; i < spawnN; i++)
        {
            GameObject _enemy = (GameObject)Instantiate(normal, spawnPoint, transform.rotation);

            _enemy.GetComponent<Enemy>().DamageValue *= waveN;
            _enemy.GetComponent<Enemy>().enemyhealth += waveN/3;
            _enemy.GetComponent<Enemy>().GoldValue *= waveN;
            yield return new WaitForSeconds(0.5f);
        }

        for (int j = 0; j < spawnF; j++)
        {
            GameObject _enemy = (GameObject)Instantiate(fast, spawnPoint, transform.rotation);
            _enemy.GetComponent<Enemy>().DamageValue *= waveN;
            _enemy.GetComponent<Enemy>().enemyhealth += waveN / 3;
            _enemy.GetComponent<Enemy>().GoldValue *= waveN;
            yield return new WaitForSeconds(0.5f);
        }

        for (int k = 0; k < spawnS; k++)
        {
            GameObject _enemy = (GameObject)Instantiate(strong, spawnPoint, transform.rotation);
            _enemy.GetComponent<Enemy>().DamageValue *= waveN;
            _enemy.GetComponent<Enemy>().enemyhealth += waveN / 3;
            _enemy.GetComponent<Enemy>().GoldValue *= waveN;
            yield return new WaitForSeconds(0.5f);
        }

        //GameObject _pan = (GameObject)Object.Instantiate(PanelPrefab, PanelStart);
        //PanelText.text = string.Format("Wave Number: {0} \n {1} Normal Enemies \n {2} Fast Enemies \n {3} Strong Enemies", waveNumber, spawnN, spawnF, spawnS);

        if(waveNumber <= maxWave)
            waveNumber++;
    }
}




