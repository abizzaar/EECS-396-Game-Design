using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavePanel : MonoBehaviour
{
    public int numNormal;
    public int numFast;
    public int numStrong;
    private Text _waveText;
    public int maxWaves;
    public int waveNumber;

    
    private float t;
    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 startPosition;
    private float timeToReachTarget = 10f;
    private float time = 10f;

    public Boolean wait;



    void Start()
    {
        startPosition = transform.position;
        waveNumber = 1;
        maxWaves = 3;
        wait = true;

    }

    void Update()
    {
        
        if (!wait)
        {
            t += Time.deltaTime / timeToReachTarget;
            PanelMove(t);
            time -= Time.deltaTime;
        }
        
        if (time < 0.001 && waveNumber<maxWaves)
        {
            transform.position = startPosition;
            t = 0;
            time = 10f;
            wait = true;
            //waveNumber++;
        }
    }

    public void PanelMove(float t)
    {
        transform.position = Vector3.Lerp(startPosition, target, t);
    }

    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }
}

