using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavePanel : MonoBehaviour
{
    private int numNormal;
    private int numFast;
    private int numStrong;
    private Text _waveText;
    public int maxWaves;
    private int waveNumber;

    
    private float t;
    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 startPosition;
    private float timeToReachTarget = 10f;
    private float time = 10f;



    void Start()
    {
        startPosition = transform.position;
        waveNumber = 1;
        maxWaves = 3;
    }

    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, target, t);
        time -= Time.deltaTime;
        if (time < 0.001 && waveNumber<maxWaves)
        {
            transform.position = startPosition;
            t = 0;
            time = 10f;
            waveNumber++;
        }
    }

    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }
}

