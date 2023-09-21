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

    GameObject pc;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        pc = GameObject.Find("PlayerObject");
        // old behavior
        // transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 7, 0.3f);
    }

    void FixedUpdate()
    {
        //Checking player position and moving
        if (satelliteType == "Basic")
        {
            playerPos = pc.transform.position.x;

            if (playerPos > transform.position.x)
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
            transform.position += new Vector3(0, -moveSpeed, 0);
        }

        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }

        if (satelliteType == "Drop")
        {
            if (bulletTimer > BulletCooldown && transform.position.y < 1.25)
            {
                Instantiate(myBullet, transform.position, Quaternion.identity);
                bulletTimer = 0;
            }
        }
        else if (satelliteType == "Bullet")
        {
            if (bulletTimer > BulletCooldown && transform.position.y < 1.3)
            {
                Quaternion bulletTargeting = Quaternion.LookRotation(pc.transform.position - transform.position);

                Instantiate(myBullet, transform.position, bulletTargeting);
                bulletTimer = 0;
            }
        }
        else if (satelliteType == "Bullet (Aim)")
        {
            if (bulletTimer > BulletCooldown && transform.position.y < 1.3)
            {
                Vector3 smartBulletAim = new Vector3(pc.transform.position.x + pc.GetComponent<playerController>().Xvelocity * 25, pc.transform.position.y + pc.GetComponent<playerController>().Yvelocity * 20, pc.transform.position.z);
                Quaternion bulletTargeting = Quaternion.LookRotation(smartBulletAim - transform.position);

                Instantiate(myBullet, transform.position, bulletTargeting);
                bulletTimer = 0;
            }
        }
        bulletTimer++;
    }
}
