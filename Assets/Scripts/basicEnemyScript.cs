using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemyScript : MonoBehaviour
{
    //This script handles basic things like sprites and health,
    //behaviors are handled in their own scripts.

    public int maxhealth = 3;
    int health;
    public int givenScore = 50;

    public bool UseBasicSprites = true; //Do you want to use the basic sprite system?
    public Sprite hp3, hp2, hp1; //Sprites the enemy uses for different health values
    public GameObject explosionObject; //The object the enemy turns into after being destroyed.
    GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        maxhealth = maxhealth + gameController.GetComponent<gameController>().Wave / 10;
        health = maxhealth;
    }

    void FixedUpdate()
    {
        //Changing sprites and death checking
        if (UseBasicSprites == true) //Turn UseBasicSprites off if you want to use animations or something
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
        else
        {
            if (health <= 0)
            {
                if (explosionObject != null)
                {
                    Instantiate(explosionObject, transform.position, new Quaternion());
                }
                gameController.GetComponent<gameController>().addScore(givenScore);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking for damage
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= collision.gameObject.GetComponent<playerBulletScript>().bulletDamage;
            Destroy(collision.gameObject);
        }
    }
}
