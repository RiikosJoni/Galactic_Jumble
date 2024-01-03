using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zooperExplosionScript : MonoBehaviour
{
    GameObject gC;
    gameController controlScript;

    public float moveSpeed = 0.1f; //How fast the enemy moves

    public Sprite die6, die5, die4, die3, die2, die1; //Sprites the enemy uses for it's animation
    [SerializeField] int animCooldown = 100;
    [SerializeField] int animTimer = 0;

    Rigidbody2D rb;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        rb = GetComponent<Rigidbody2D>();

        transform.position += new Vector3(0, 0, 0.5f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed;
        moveSpeed = moveSpeed * 0.95f;

        animTimer += 1;
        if (animTimer / animCooldown == Mathf.Round(animTimer / animCooldown)) //This frame change animation
        {
            if (animTimer == animCooldown)
            {
                GetComponent<SpriteRenderer>().sprite = die1;
            }
            else if (animTimer == animCooldown * 2)
            {
                GetComponent<SpriteRenderer>().sprite = die2;
            }
            else if (animTimer == animCooldown * 3)
            {
                GetComponent<SpriteRenderer>().sprite = die3;
            }
            else if (animTimer == animCooldown * 4)
            {
                GetComponent<SpriteRenderer>().sprite = die4;
            }
            else if (animTimer == animCooldown * 5)
            {
                GetComponent<SpriteRenderer>().sprite = die5;
            }
            else if (animTimer == animCooldown * 6)
            {
                GetComponent<SpriteRenderer>().sprite = die6;
            }
            else if (animTimer == animCooldown * 7)
            {
                Destroy(gameObject);
            }
        }
    }
}
