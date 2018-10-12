using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public float spawnCooldown;
    public float spawnTimer;
    public Object sE;
    public Object fE;
    public Object hE;
    public Slider timeSlider;
    public Text timerInfo;

    public float currentTimer;
    public int currentWave;
    public bool inWave;
    public float[] waveCooldowns;
    public float[] waveTimers;
    public int[] enemyCounts;
    public int enemiesSpawned;
	public Vector3[] probabilities;
    [HideInInspector]public Object[] wave;
    [HideInInspector] public int[] typeCounts;

    public GameObject waveInfoCanvas;

	// Use this for initialization
	void Start ()
	{
	    sE = Resources.Load("SimpleEnemy");
		fE = Resources.Load("FastEnemy");
		hE = Resources.Load("HeavyEnemy");
	    waveInfoCanvas = GameObject.Find("Canvas").transform.Find("WaveInfoMenu").gameObject;
        currentWave = 0;
        currentTimer = waveCooldowns[currentWave];
        timeSlider.maxValue = currentTimer;

	    probabilities = new Vector3[3];
	    probabilities[0] = new Vector3(0.75f, 0.25f, 0.0f);
	    probabilities[1] = new Vector3(0.6f, 0.3f, 0.1f);
	    probabilities[2] = new Vector3(0.4f, 0.3f, 0.3f);
	}

    void Awake()
    {
        Globals.em = this;
    }

    // Update is called once per frame
    void Update () {
        currentTimer -= Time.deltaTime;
        timeSlider.value = timeSlider.maxValue - currentTimer;
        if (currentTimer <= 0.0f)
        {
            if (inWave)
            {
                currentWave++;
                if (currentWave >= 3) {
                    Globals.pm.forDaWin();
                    return;
                }
             
                currentTimer = waveCooldowns[currentWave];
                timerInfo.text = "Time until next wave";
                enemiesSpawned = 0;
                timeSlider.gameObject.SetActive(true);

            } else
            {
                timeSlider.gameObject.SetActive(false);
                wave = new Object[enemyCounts[currentWave]];
                typeCounts = new int[3];
                for (int i = 0; i < enemyCounts[currentWave]; i++)
                {
                    float r = Random.value;
                    if (r < probabilities[currentWave].x)
                    {
                        wave[i] = sE;
                        typeCounts[0]++;
                    } else if (r < probabilities[currentWave].x + probabilities[currentWave].y)
                    {
                        wave[i] = fE;
                        typeCounts[1]++;
                    }
                    else
                    {
                        wave[i] = hE;
                        typeCounts[2]++;
                    }
                }
                currentTimer = waveTimers[currentWave];
                spawnCooldown = waveTimers[currentWave] / (float)wave.Length;
                spawnTimer = 0.0f;

                waveInfoCanvas.transform.Find("waveNumber").GetComponent<Text>().text = "Wave #: " + (currentWave + 1f);
                waveInfoCanvas.transform.Find("enemyInfo").transform.Find("sT").GetComponent<Text>().text = ": " + typeCounts[0];
                waveInfoCanvas.transform.Find("enemyInfo").transform.Find("fT").GetComponent<Text>().text = ": " + typeCounts[1];
                waveInfoCanvas.transform.Find("enemyInfo").transform.Find("hT").GetComponent<Text>().text = ": " + typeCounts[2];
                timerInfo.text = "Time until wave ends";
            }
            timeSlider.maxValue = currentTimer;
            inWave = !inWave;
        }

        if (inWave)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0.0f)
            {
                GameObject enemy = (GameObject)Object.Instantiate(wave[enemiesSpawned], transform.position, transform.rotation);
                if (enemy.GetComponent<SimpleEnemy>() != null)
                {
                    typeCounts[0]--;
                    waveInfoCanvas.transform.Find("enemyInfo").transform.Find("sT").GetComponent<Text>().text = ": " + typeCounts[0];
                } else if (enemy.GetComponent<FastEnemy>() != null)
                {
                    typeCounts[1]--;
                    waveInfoCanvas.transform.Find("enemyInfo").transform.Find("fT").GetComponent<Text>().text = ": " + typeCounts[1];
                }
                else
                {
                    typeCounts[2]--;
                    waveInfoCanvas.transform.Find("enemyInfo").transform.Find("hT").GetComponent<Text>().text = ": " + typeCounts[2];
                }
                enemiesSpawned++;
                spawnTimer = spawnCooldown;
            }
        }
	}
}
