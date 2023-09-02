using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private gameController gameController;

    public float Xvelocity = 0f;
    public float Yvelocity = 0f;
    public float maxXVelocity = 0.1f;
    public float maxYVelocity = 0.1f;
    public float acceleration = 0.06f;
    public float friction = 0.08f;

    public int maxHealth = 3;
    public int health = 3;

    public string PlayerType = "Basic";
    public string PlayerAbility;

    public GameObject bullet;
    int bulletTimer = 0;
    public int bulletRate = 3;
    public int bulletCount = 1;

    void FixedUpdate()
    {
        
    }
}
