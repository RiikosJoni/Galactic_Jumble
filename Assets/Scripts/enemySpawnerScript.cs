using System;
using System.Collections;
using UnityEngine;
using System.Text.RegularExpressions;

public class enemySpawnerScript : MonoBehaviour
{
    public gameController gameController;

    //Each entry in spawnlist corresponds to a wave. The numbers are the entries in enemylist (eq 0,0 = satellite_basic, satellite_basic)
    //0 - Satellite, 1 - Satellite Bullet, 2 - Satellite Aim, 3 - Satellite Burst, 4 - Satellite Burst Aim, 5 - Satellite Line, 6 - Satellite Drop, 7 - Satellite Armored, 8 - Satellite Spike, 9 - Satellite Napalm
    string[] enemyList = { "Satellite_Basic", "Satellite_Bullet", "Satellite_Aim", "Satellite_Burst", "Satellite_Burst_Aim", "Satellite_Line", "Satellite_Drop" };
    string[] spawnList = { "0,0,0,0", "0b,0,0b", "0b,0,6bj,0,0k", "0m,6n,6or,6p,6q" };

    [SerializeField] public int EnemyCooldown = 100;
    [SerializeField] public int EnemyMultiplier = 1;

    Vector3 SpawnPos;
    [SerializeField] bool PosModified = false;

    public IEnumerator EnemySpawn()
    {
        while (gameController.isWaveActive == true && gameController.isPaused == false)
        {
            string latestSpawnList;

            if (gameController.Wave - 1 < spawnList.Length)
            {
                latestSpawnList = spawnList[gameController.Wave - 1];
                Debug.Log("Current wave's spawnlist: " + latestSpawnList);
            }
            else
            {
                latestSpawnList = "0,0,0,0,0j,0k,0l,6l,0l"; //placeholder wave
            }

            string[] enemyListSplit = latestSpawnList.Split(',');
            Debug.Log("Enemies in this wave: " + enemyListSplit.Length);

            for (int i = 0; i < enemyListSplit.Length; i++)
            {
                //Parsing gameobject to spawn
                string parsedEnemyNumStr = Regex.Replace(enemyListSplit[i], "[^0-9]", "");
                int parsedEnemyNum = int.Parse(parsedEnemyNumStr);
                Debug.Log("Starting enemy spawning, current enemy id: " + parsedEnemyNum);

                //Parsing spawning settings
                string parsedSpawnSetting = Regex.Replace(enemyListSplit[i], @"[\d-]", string.Empty);
                Debug.Log("Current spawning settings: " + parsedSpawnSetting);

                SpawnPos = new Vector3(0, 2, 0);
                if (parsedSpawnSetting.Contains("a")) { EnemyCooldown += 50; } //Add longer cooldown, can be stacked (abc = 450 cooldown)
                else if (parsedSpawnSetting.Contains("b")) { EnemyCooldown += 100; }
                else if (parsedSpawnSetting.Contains("c")) { EnemyCooldown += 200; }
                else if (parsedSpawnSetting.Contains("d")) { EnemyCooldown += 400; }
                else if (parsedSpawnSetting.Contains("e")) { EnemyCooldown -= 5; } //Shorten cooldown, can be stacked (fgh = 15 cooldown)
                else if (parsedSpawnSetting.Contains("f")) { EnemyCooldown -= 10; }
                else if (parsedSpawnSetting.Contains("g")) { EnemyCooldown -= 25; }
                else if (parsedSpawnSetting.Contains("h")) { EnemyCooldown -= 50; }
                else if (parsedSpawnSetting.Contains("i")) { EnemyCooldown -= 99; }
                else if (parsedSpawnSetting.Contains("j")) { EnemyMultiplier = 2; } //Changes how many enemies of type get spawned at once
                else if (parsedSpawnSetting.Contains("k")) { EnemyMultiplier = 3; }
                else if (parsedSpawnSetting.Contains("l")) { EnemyMultiplier = 5; }
                else if (parsedSpawnSetting.Contains("m")) { SpawnPos = new Vector3(-1, 2, 0); PosModified = true; } //Spawn at the far left side of the screen      NOTE: DONT STACK THESE WITH EACH OTHER OR ENEMY MULTIPLIERS
                else if (parsedSpawnSetting.Contains("n")) { SpawnPos = new Vector3(-0.5f, 2, 0); PosModified = true; } //Spawn at the left side of the screen
                else if (parsedSpawnSetting.Contains("o")) { SpawnPos = new Vector3(0, 2, 0); PosModified = true; } //Spawn at the middle of the screen
                else if (parsedSpawnSetting.Contains("p")) { SpawnPos = new Vector3(0.5f, 2, 0); PosModified = true; } //Spawn at the right side of the screen
                else if (parsedSpawnSetting.Contains("q")) { SpawnPos = new Vector3(1, 2, 0); PosModified = true; } //Spawn at the far right side of the screen
                else if (parsedSpawnSetting.Contains("r")) { SpawnPos += new Vector3(0, 2, 0); } //Spawn farther away from screen

                Debug.Log("The enemy to spawn is: " + enemyList[parsedEnemyNum]); //Spawning enemy
                if (Resources.Load("Enemies/" + enemyList[parsedEnemyNum]))
                {
                    Debug.Log(enemyList[parsedEnemyNum] + ".prefab was found!");
                    for (int m = 0; m < EnemyMultiplier; m++)
                    {
                        if (PosModified == false)
                        {
                            SpawnPos += new Vector3(UnityEngine.Random.Range(-1.2f, 1.2f), 0, 0);
                        }
                        Instantiate(Resources.Load("Enemies/" + enemyList[parsedEnemyNum]), SpawnPos, Quaternion.identity);
                    }
                }
                else { Debug.Log(enemyList[parsedEnemyNum] + ".prefab was not found!"); }

                yield return new WaitForSeconds(EnemyCooldown / 100);
                EnemyCooldown = 100;

                PosModified = false;
                EnemyMultiplier = 1;
            }

            gameController.isWaveActive = false;
            Debug.Log("Wave " + gameController.Wave + " ended!");
        }
    }
}
