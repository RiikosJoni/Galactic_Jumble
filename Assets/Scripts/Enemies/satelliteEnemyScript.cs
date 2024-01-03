using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (satelliteType == "Drop") //Drops bullet with cooldown
        {
            if (bulletTimer > BulletCooldown && transform.position.y < 1.25)
            {
                Instantiate(myBullet, transform.position, Quaternion.identity);
                bulletTimer = 0;
            }
        }
        else if (satelliteType == "Bullet") //Fires bullet at the player
        {
            if (bulletTimer > BulletCooldown && transform.position.y < 1.3)
            {
                Quaternion bulletTargeting = Quaternion.LookRotation(pObj.transform.position - transform.position);

                Instantiate(myBullet, transform.position, bulletTargeting);
                bulletTimer = 0;
            }
        }
        else if (satelliteType == "Bullet (Aim)") //Attepts to shoot the bullet where the player will be. Emphazising on the attemps-part
        {
            if (bulletTimer > BulletCooldown && transform.position.y < 1.3)
            {
                Vector3 smartBulletAim = new Vector3(pObj.transform.position.x + pObj.GetComponent<playerController>().Xvelocity * 25, pObj.transform.position.y + pObj.GetComponent<playerController>().Yvelocity * 20, pObj.transform.position.z);
                Quaternion bulletTargeting = Quaternion.LookRotation(smartBulletAim - transform.position);

                Instantiate(myBullet, transform.position, bulletTargeting);
                bulletTimer = 0;
            }
        }
        bulletTimer++;
    }
}
