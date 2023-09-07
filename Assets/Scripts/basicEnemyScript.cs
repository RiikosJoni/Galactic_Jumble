using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemyScript : MonoBehaviour
{
    public int maxhealth = 3;
    int health;

    public Sprite hp3, hp2, hp1;
    public GameObject explosionObject;

    //This script only handles things like sprites, behaviors are handled in their own scripts.

    void Start()
    {
        health = maxhealth;
    }

    void FixedUpdate()
    {
        if (health < 0)
        {
            if (explosionObject != null) 
            {
                Instantiate(explosionObject, transform.position, new Quaternion());
            }
            Destroy(gameObject);
        }
        else if (health < maxhealth * 0.33f)
        {
            GetComponent<SpriteRenderer>().sprite = hp1;
        }
        else if (health >= maxhealth * 0.33f)
        {
            GetComponent<SpriteRenderer>().sprite = hp2;
        }
        else if (health > maxhealth * 0.7f)
        {
            GetComponent<SpriteRenderer>().sprite = hp3;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= 1;
            Destroy(collision.gameObject);
        }
    }
}
