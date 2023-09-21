using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public gameController gameController;
    public cameraControlScript camController;

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
    public int bulletRate = 10;
    public int bulletCount = 1;
    public int bulletDamage = 1;

    [SerializeField] int invFrames = 0;
    GameObject textManager;

    private void Start()
    {
        textManager = GameObject.Find("Canvas");
    }

    void FixedUpdate()
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

        if (Input.GetKey("z") && bulletTimer > bulletRate)
        {
            ShootBullet();
        }
        bulletTimer++;

        //Reduces invulnerability frames
        invFrames -= 1;
    }

    private void ShootBullet()
    {
        for (int bulletsFired = 0; bulletsFired < bulletCount; bulletsFired++)
        {
            Vector3 bulletPos;
            if (bulletCount == 1)
            {
                bulletPos = transform.position;
            }
            else
            {
                bulletPos = transform.position + new Vector3((-(float)bulletCount / 40) + ((float)bulletCount / 20) / (bulletCount - 1) * bulletsFired, 0, 0);
            }
            GameObject latestBullet = Instantiate(bullet, bulletPos, transform.rotation);

            latestBullet.GetComponent<playerBulletScript>().bulletType = PlayerType;
            latestBullet.GetComponent<playerBulletScript>().bulletDamage = bulletDamage;
        }

        bulletTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invFrames < 1)
        {
            health -= 1;
            invFrames = 30;
            textManager.GetComponent<textManager>().CounterRefresh();
            camController.shake = 15;
        }else if (collision.gameObject.CompareTag("Bullet") && invFrames < 1)
        {
            health -= 1;
            invFrames = 30;
            textManager.GetComponent<textManager>().CounterRefresh();
            Destroy(collision.gameObject);
            camController.shake = 15;
        }
    }
}
