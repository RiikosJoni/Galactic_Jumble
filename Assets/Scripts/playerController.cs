using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public gameController gameController;

    [SerializeField]
    float Xvelocity = 0f;
    [SerializeField]
    float Yvelocity = 0f;
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
        if (!gameController.isPaused) 
        {
            //Add friction
            Xvelocity = Xvelocity * (1 - friction);
            Yvelocity = Yvelocity * (1 - friction);

            //Limiting movement speed
            if (Xvelocity > maxXVelocity)
            {
                Xvelocity = maxXVelocity;
            }
            else if (Xvelocity < maxXVelocity * -1)
            {
                Xvelocity = -maxXVelocity;
            }
            if (Yvelocity > maxYVelocity)
            {
                Yvelocity = maxYVelocity;
            }
            else if (Yvelocity < maxYVelocity * -1)
            {
                Yvelocity = -maxYVelocity;
            }

            //Limiting Position
            if (transform.position.y > 1.45 && Yvelocity > 0)
            {
                Yvelocity = 0;
            }
            if (transform.position.y < -1.45 && Yvelocity < 0)
            {
                Yvelocity = 0;
            }
            if (transform.position.x > 1.37 && Xvelocity > 0)
            {
                Xvelocity = 0;
            }
            if (transform.position.x < -1.37 && Xvelocity < 0)
            {
                Xvelocity = 0;
            }

            //Move Object
            transform.position += new Vector3(Xvelocity, Yvelocity, 0);

            //Input Management!!!! :D
            if (Input.GetKey("up"))
            {
                Yvelocity += acceleration;
            }
            if (Input.GetKey("down"))
            {
                Yvelocity -= acceleration;
            }
            if (Input.GetKey("right"))
            {
                Xvelocity += acceleration;
            }
            if (Input.GetKey("left"))
            {
                Xvelocity -= acceleration;
            }
        }
    }
}
