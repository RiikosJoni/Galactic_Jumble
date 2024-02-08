using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class satelliteEnemyScript : MonoBehaviour
{
    //This script handles the behaviors most of the satellite-type enemies

    GameObject gC;
    gameController controlScript;

    public float moveSpeed = 0.1f; //How fast moves down / homes in on the player
    float playerPos;

    public string satelliteType = "null";
    public GameObject myBullet; //What the satellite shoots if shoots
    public int BulletCooldown = 100;
    [SerializeField] int bulletTimer = 0;

    GameObject pObj; //the PlayerObject

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        pObj = GameObject.Find("PlayerObject");

        // old behavior
        // transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 7, 0.3f);
    }

    void FixedUpdate()
    {
        //Checking player position and moving
        if (satelliteType == "Basic")
        {
            playerPos = pObj.transform.position.x; //Checks for player position

            if (playerPos > transform.position.x) //Moves towards player
            {
                transform.position += new Vector3(moveSpeed / 30, -moveSpeed, 0);
            }
            else
            {
                transform.position += new Vector3(-moveSpeed / 30, -moveSpeed, 0);
            }
        }
        else
        {
            transform.position += new Vector3(0, -moveSpeed, 0); //Shooting satellites dont home in on the player
        }

        if (transform.position.y < -2) //Destroys satellite if off screen
        {
            Destroy(gameObject);
        }

        switch (satelliteType)
        {
            case "Drop"://Drops bullet with cooldown
                if (bulletTimer > BulletCooldown && transform.position.y < 1.35)
                {
                    Instantiate(myBullet, transform.position, Quaternion.identity);
                    bulletTimer = 0;
                }
                break;
            case "Bullet"://Fires bullet at the player
                if (bulletTimer > BulletCooldown && transform.position.y < 1.4)
                {
                    Vector3 difference = pObj.transform.position - transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    bulletTimer = 0;
                }
                break;
            case "Bullet (Aim)"://Attepts to shoot the bullet where the player will be. Emphazising on the attemps-part
                if (bulletTimer > BulletCooldown && transform.position.y < 1.4)
                {
                    Vector3 difference = new Vector3(pObj.transform.position.x + pObj.GetComponent<playerController>().Xvelocity * (pObj.transform.position.x - transform.position.x) * 20 - transform.position.x, pObj.transform.position.y + pObj.GetComponent<playerController>().Yvelocity * (pObj.transform.position.x - transform.position.x) * 20 - transform.position.y, pObj.transform.position.z);
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    bulletTimer = 0;
                }
                break;
            case "Burst 3":
                if (bulletTimer > BulletCooldown && transform.position.y < 1.4)
                {
                    Vector3 difference = pObj.transform.position - transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 15));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 15));
                    bulletTimer = 0;
                }
                break;
            case "Burst 5":
                if (bulletTimer > BulletCooldown && transform.position.y < 1.4)
                {
                    Vector3 difference = pObj.transform.position - transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 15));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 7));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 7));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 15));
                    bulletTimer = 0;
                }
                break;
            case "Burst 3/2":
                if (bulletTimer > BulletCooldown && transform.position.y < 1.4)
                {
                    Vector3 difference = pObj.transform.position - transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 10));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 10));

                    bulletTimer = 0;
                }else if (bulletTimer > BulletCooldown - 7 && bulletTimer <= BulletCooldown - 6 && transform.position.y < 1.3)
                {
                    Vector3 difference = pObj.transform.position - transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 20));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    Instantiate(myBullet, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 20));
                }
                break;
        }

        if (transform.position.y < 1.6)
        {
            bulletTimer++;
        }
    }
}
