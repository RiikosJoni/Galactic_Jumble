using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public int Score = 0;
    public int Wave = 0;
    public bool isWaveActive = false;

    public enemySpawnerScript enemySpawner;

    public bool isPaused = false;

    void Start()
    {
        Debug.Log("Scene initaliazied"); //There might be typos it's midninght right now

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
            Wave++;
            isWaveActive = true;
            Debug.Log("Wave " + Wave + " started!");

            if (enemySpawner != null)
            {
                StartCoroutine(enemySpawner.EnemySpawn());
            }
        }
    }
}
