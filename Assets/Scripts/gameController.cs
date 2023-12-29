using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class gameController : MonoBehaviour
{
    //List of difficulties: 
    //	-Cheater's Paradise (0)
    //		What are you doing here?
    //	-Baby's First Playthrough (1)
    //		Really easy. Health spawns more often. No last final boss phase
    //	-A Quite Regular Experience
    //		The default difficulty. Not too challenging. (2)
    //	-Approved by Friedrich Mohs
    //		Harder enemies and less health pickups. (3)
    //	-Extremely Unfriendly (4)
    //      The intended difficulty. Limited max health (3), harder attacks. Gaining extra max health instead decreases ability cooldown
    //	-Clinically Insane (5)
    //      Same as Extremely Unfriendly, but multiplied by 10, basically
    //	-Armageddon Adventure (6?)
    //      Maybe not

    public int Difficulty = 2;
    public int Score = 0;
    public int Wave = 0;
    public bool isWaveActive = false; //Are enemies currently spawning? Used for between-the waves cutscenes or powerups.

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

        if (Input.GetKeyDown("y")) //Debug stuff. Adds 1 to wave counter and starts wave.
        {
            addWave();
            isWaveActive = true;
            Debug.Log("Wave " + Wave + " started!");

            if (enemySpawner != null)
            {
                StartCoroutine(enemySpawner.EnemySpawn());
            }
        }

        if (Input.GetKeyDown("b")) //Debug stuff. Spawns wave 10000.
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
