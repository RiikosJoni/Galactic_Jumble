using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public int Score = 0;
    public int Wave = 0;
    public bool isWaveActive = false;

    public enemySpawnerScript enemySpawner;
    public textManager textManager;

    public bool isPaused = false;

    void Start()
    {
        Debug.Log("Scene initaliazied");

}

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (isPaused) { isPaused = false; } else { isPaused = true; }
            Debug.Log("Pause Toggled: " + isPaused);
        }

        if (Input.GetKeyDown("y"))
        {
            addWave();
            isWaveActive = true;
            Debug.Log("Wave " + Wave + " started!");

            if (enemySpawner != null)
            {
                StartCoroutine(enemySpawner.EnemySpawn());
            }
        }

        if (Input.GetKeyDown("b"))
        {
            Wave = 10000;
            isWaveActive = true;
            Debug.Log("Wave " + Wave + " started!");

            if (enemySpawner != null)
            {
                StartCoroutine(enemySpawner.EnemySpawn());
            }
        }
    }

    public void addWave()
    {
        Wave++;
        int waveInt = (int)Mathf.Round(Wave);
        textManager.GetComponent<textManager>().CounterRefresh();
    }

    public void addScore(int addedScore)
    {
        Score += addedScore;
        textManager.GetComponent<textManager>().CounterRefresh();
    }
}
