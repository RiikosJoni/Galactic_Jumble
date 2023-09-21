using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemyScript : MonoBehaviour
{
    public int maxhealth = 3;
    int health;
    public int givenScore = 50;

    public Sprite hp3, hp2, hp1;
    public GameObject explosionObject;
    GameObject gameController;
    //This script only handles things like sprites, behaviors are handled in their own scripts.

    void Start()
    {
        gameController = GameObject.Find("GameController");
        maxhealth = maxhealth + gameController.GetComponent<gameController>().Wave / 10;
        health = maxhealth;
    }

    void FixedUpdate()
    {
        if (health > maxhealth * 0.7f)
        {
            GetComponent<SpriteRenderer>().sprite = hp3;
        }
        else if (health >= maxhealth * 0.34f)
        {
            GetComponent<SpriteRenderer>().sprite = hp2;
        }
        else if (health < maxhealth * 0.34f && health > 0)
        {
            GetComponent<SpriteRenderer>().sprite = hp1;
        }
        else
        {
            if (explosionObject != null)
            {
                Instantiate(explosionObject, transform.position, new Quaternion());
            }
            gameController.GetComponent<gameController>().addScore(givenScore);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= collision.gameObject.GetComponent<playerBulletScript>().bulletDamage;
            Destroy(collision.gameObject);
        }
    }
}
