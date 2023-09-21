using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class gameController : MonoBehaviour
{
    //Difficultíes:
    //0 - Easy
    //1 - Normal
    //2 - Hard
    //3 - Cool Mode
    //4 - Impossible
    public int Difficulty = 2;
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
            if (isPaused) { isPaused = false; Time.timeScale = 1; } else { isPaused = true; Time.timeScale = 0; }
            Debug.Log("Pause Toggled: " + isPaused);
        }

        if (Input.GetKeyDown("o"))
        {
            Time.timeScale = Time.timeScale / 2;
        }

        if (Input.GetKeyDown("p"))
        {
            Time.timeScale = Time.timeScale * 2;
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
