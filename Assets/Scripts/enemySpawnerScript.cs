using System;
using System.Collections;
using UnityEngine;
using System.Text.RegularExpressions;

public class enemySpawnerScript : MonoBehaviour
{
    public gameController gameController;

    //Each entry in spawnlist corresponds to a wave. The numbers are the entries in enemylist (eq 0,0 = satellite_basic, satellite_basic)
    //0 - Satellite, 1 - Satellite Bullet, 2 - Satellite Aim, 3 - Satellite Burst, 4 - Satellite Burst Aim, 5 - Satellite Line, 6 - Satellite Drop, 7 - Satellite Armored, 8 - Satellite Spike, 9 - Satellite Napalm, 10 - Zooper :)
    string[] enemyList = { "Satellite_Basic", "Satellite_Bullet", "Satellite_Aim", "Satellite_Burst_3", "Satellite_Burst_5", "Satellite_Burst_32", "Satellite_Line", "Satellite_Drop", "Satellite_Armored", "Satellite_Spike", "Satellite_Napalm", "Zooper" };
    string[] spawnList = { "0mn,0pq,0c,0io,0r,1cm,1p", "0b,0,0b", "0b,0,6bj,0,0k", "0m,6n,6or,6p,6q", "0m,1n,11or,2p,11k"};

    [SerializeField] public int EnemyCooldown = 100;
    [SerializeField] public int EnemyMultiplier = 1;

    Vector3 SpawnPos;

    public IEnumerator EnemySpawn()
    {
        while (gameController.isWaveActive == true)
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

                int SpawnOffset = 0;
                bool SpawnRandom = false;

                if (parsedSpawnSetting.Contains("a")) { EnemyCooldown += 50; } //Add longer cooldown, can be stacked (abc = 450 cooldown)
                if (parsedSpawnSetting.Contains("b")) { EnemyCooldown += 100; }
                if (parsedSpawnSetting.Contains("c")) { EnemyCooldown += 200; }
                if (parsedSpawnSetting.Contains("d")) { EnemyCooldown += 400; }

                if (parsedSpawnSetting.Contains("e")) { EnemyCooldown -= 5; } //Shorten cooldown, can be stacked (fgh = 15 cooldown)
                if (parsedSpawnSetting.Contains("f")) { EnemyCooldown -= 10; }
                if (parsedSpawnSetting.Contains("g")) { EnemyCooldown -= 25; }
                if (parsedSpawnSetting.Contains("h")) { EnemyCooldown -= 50; }
                if (parsedSpawnSetting.Contains("i")) { EnemyCooldown -= 99; }

                if (parsedSpawnSetting.Contains("j")) { EnemyMultiplier = 2; } //Changes how many enemies of type get spawned at once
                else if (parsedSpawnSetting.Contains("k")) { EnemyMultiplier = 3; }
                else if (parsedSpawnSetting.Contains("l")) { EnemyMultiplier = 5; }

                if (parsedSpawnSetting.Contains("m")) { SpawnOffset += 1; }//Changes where the enemy spawns
                if (parsedSpawnSetting.Contains("n")) { SpawnOffset += 2; }
                if (parsedSpawnSetting.Contains("o")) { SpawnOffset += 4; }
                if (parsedSpawnSetting.Contains("p")) { SpawnOffset -= 1; }
                if (parsedSpawnSetting.Contains("q")) { SpawnOffset -= 2; }
                if (parsedSpawnSetting.Contains("r")) { SpawnOffset -= 4; }

                SpawnPos = new Vector3(SpawnOffset * 0.25f, 2, 0);

                Debug.Log("The enemy to spawn is: " + enemyList[parsedEnemyNum]); //Spawning enemy
                if (Resources.Load("Enemies/" + enemyList[parsedEnemyNum]))
                {
                    Debug.Log(enemyList[parsedEnemyNum] + ".prefab was found!");
                    for (int m = 0; m < EnemyMultiplier; m++)
                    {
                        Instantiate(Resources.Load("Enemies/" + enemyList[parsedEnemyNum]), SpawnPos, Quaternion.identity);
                    }
                }
                else { Debug.Log(enemyList[parsedEnemyNum] + ".prefab was not found!"); }

                yield return new WaitForSeconds(EnemyCooldown / 100);
                EnemyCooldown = 100;

                EnemyMultiplier = 1;
            }

            gameController.isWaveActive = false;
            Debug.Log("Wave " + gameController.Wave + " ended!");
        }
    }
}
