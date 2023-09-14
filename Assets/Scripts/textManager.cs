using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class textManager : MonoBehaviour
{
    public gameController gameController;
    public playerController playerController;

    public Text WaveCounter;
    public Text ScoreCounter;
    public Text HealthCounterDEBUG;

    void Start()
    {
        WaveCounter.text = "Wave: " + gameController.Wave.ToString();
        ScoreCounter.text = "Score: " + gameController.Score.ToString();
        HealthCounterDEBUG.text = "DebugHP = " + playerController.health.ToString();
    }

    public void CounterRefresh()
    {
        WaveCounter.text = "Wave: " + gameController.Wave.ToString();
        ScoreCounter.text = "Score: " + gameController.Score.ToString();
        HealthCounterDEBUG.text = "DebugHP = " + playerController.health.ToString();
    }
}
