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
            _enemy.GetComponent<NavMeshAgent>().Warp(spawnPoint);
            _enemy.GetComponent<Enemy>().DamageValue *= waveN;
            _enemy.GetComponent<Enemy>().enemyhealth += waveN/3;
            _enemy.GetComponent<Enemy>().GoldValue *= waveN;
            yield return new WaitForSeconds(0.5f);
        }

        for (int j = 0; j < spawnF; j++)
        {
            GameObject _enemy = (GameObject)Instantiate(fast, spawnPoint, transform.rotation);
            _enemy.GetComponent<NavMeshAgent>().Warp(spawnPoint);
            _enemy.GetComponent<Enemy>().DamageValue *= waveN;
            _enemy.GetComponent<Enemy>().enemyhealth += waveN / 3;
            _enemy.GetComponent<Enemy>().GoldValue *= waveN;
            yield return new WaitForSeconds(0.5f);
        }

        for (int k = 0; k < spawnS; k++)
        {
            GameObject _enemy = (GameObject)Instantiate(strong, spawnPoint, transform.rotation);
            _enemy.GetComponent<NavMeshAgent>().Warp(spawnPoint);
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





/*



public Object[] enemies;
private float respawn;
private Vector3 spawnpoint;
private GameObject pbase;
private NavMeshAgent agent;
private Transform target;
//public bool clearpath;
//private NavMeshPath path = new NavMeshPath();
private float waveWaitTime = 5f;
//private float enemyWaitTime = 1f;
public int waveNumber;
private bool waveRunning;

public GameObject PanelPrefab;
private Transform PanelStart;



// Use this for initialization
void Start()
{
    waveCooldown = 10f;  //Startup CD

    //Enemies go along the ground or wherever we put the base
    pbase = GameObject.Find("Base");
    spawnpoint = transform.position;
    //StartCoroutine(CurrentWave());

    waveRunning = false;

    waveNumber = 1;
    target = GameObject.Find("Base").transform;

    //agent = GetComponent<NavMeshAgent>();
    //agent.CalculatePath(target.position, path);

    PanelStart = PanelPrefab.transform;
}

// Update is called once per frame
void Update()
{
    if (waveCooldown >= 0)
    {
        waveCooldown -= Time.time;
        return;
    }

    if (!waveRunning)
    {
        float spawntime = Time.time;
        if (spawntime > respawn + waveWaitTime)
        {
            StopCoroutine(CurrentWave());
            waveRunning = true;
            waveNumber = waveNumber + 1;
            StartCoroutine(CurrentWave());
        }
    }

    
    //if (path.status == NavMeshPathStatus.PathPartial)
    //{
    //    clearpath = false;
    //}
    



}

IEnumerator CurrentWave()
{
    int numberOfEnemiesNormal = Random.Range(2, 5);
    int numberOfEnemiesStrong = Random.Range(1, 3);
    int numberOfEnemiesFast = Random.Range(1, 3);
    int totalEnemies = numberOfEnemiesFast + numberOfEnemiesNormal + numberOfEnemiesStrong;
    List<Object> enemiesInWave = new List<Object>();

    GameObject _pan = (GameObject)Object.Instantiate(PanelPrefab, PanelStart);
    _pan.GetComponentInChildren<Text>().text = string.Format("Wave Number: {0} \n {1} Normal Enemies \n {2} Fast Enemies \n {3} Strong Enemies", waveNumber, numberOfEnemiesNormal, numberOfEnemiesFast, numberOfEnemiesStrong);

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
        GameObject _enemy = (GameObject)Object.Instantiate(enemiesInWave[indexOfCurrEnemy], spawnpoint, transform.rotation, gameObject.transform);
        var _enemyScript = _enemy.GetComponent<Enemy>();
        _enemyScript.DamageValue = _enemyScript.DamageValue * waveNumber;
        _enemyScript.enemyhealth= _enemyScript.enemyhealth * waveNumber / 2;
        _enemyScript.GoldValue = _enemyScript.GoldValue * waveNumber;
        enemiesInWave[indexOfCurrEnemy] = enemiesInWave[currSize - 1];
        currSize = currSize - 1;
        yield return new WaitForSeconds(enemyWaitTime);
    }
    waveRunning = false;
    respawn = Time.time;
    waveWaitTime = (Random.value * 2) + 5;
}
}

*/
      