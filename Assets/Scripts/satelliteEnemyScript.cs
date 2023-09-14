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
    public int BulletCooldown = 100;
    [SerializeField] int bulletTimer = 0;

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
            if (satelliteType == "Basic")
            {
                playerPos = GameObject.Find("PlayerObject").transform.position.x;

                if (playerPos > transform.position.x)
                {
                    transform.position += new Vector3(moveSpeed / 10, -moveSpeed, 0);
                }
                else
                {
                    transform.position += new Vector3(-moveSpeed / 10, -moveSpeed, 0);
                }
            }else
            {
                transform.position += new Vector3(0, -moveSpeed, 0);
            }

            if (transform.position.y < -2)
            {
                Destroy(gameObject);
            }

            if (satelliteType == "Drop")
            {
                if(bulletTimer > BulletCooldown && transform.position.y < 1.25)
                {
                    Instantiate(myBullet, transform.position, Quaternion.identity); 
                    bulletTimer = 0;
                }
            }
            bulletTimer++;
        }
    }
}
