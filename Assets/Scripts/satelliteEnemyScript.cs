using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satelliteEnemyScript : MonoBehaviour
{
    GameObject gC;
    gameController controlScript;

    public float moveSpeed = 0.1f;
    float playerPos;

    public string satelliteType = "null";
    public GameObject myBullet;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();
        // old behavior
        // transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 7, 0.3f);
    }

    void FixedUpdate()
    {
        if (!controlScript.isPaused)
        {
            //Checking player position and moving
            playerPos = GameObject.Find("PlayerObject").transform.position.x;

            if (playerPos > transform.position.x)
            {
                transform.position += new Vector3(moveSpeed / 10, -moveSpeed, 0);
            }
            else
            {
                transform.position += new Vector3(-moveSpeed / 10, -moveSpeed, 0);
            }

            if (transform.position.y < -2)
            {
                Destroy(gameObject);
            }
        }
    }
}
