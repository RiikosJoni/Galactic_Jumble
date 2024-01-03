using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class zooperEnemyScript : MonoBehaviour
{
    //This script handles the behaviors the Zooper, the best enemy in the game :)

    GameObject gC;
    gameController controlScript;

    basicEnemyScript basicEnemyScript;

    public int startCooldown = 100; //The time between it reaching the launching height on the screen and launching at the player
    public float preLaunchSpeed = 0.1f; //How fast it moves before launching off
    [SerializeField] bool hasLaunched = false;

    public float moveSpeed = 0.1f; //How fast the enemy moves
    public float rotSpeed = 0.1f; //How fast the enemy rotates

    Vector2 dir;
    float rotAmount;

    public Sprite spin4, spin3, spin2, spin1; //Sprites the enemy uses for it's animation
    [SerializeField] int animCooldown = 100;
    [SerializeField] int animTimer = 0;

    Rigidbody2D rb;

    public GameObject explosionObject;

    Transform pObj; //the PlayerObject
    float playerXPos;
    float playerYPos;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        basicEnemyScript = GetComponent<basicEnemyScript>();
        rb = GetComponent<Rigidbody2D>();

        pObj = GameObject.FindGameObjectWithTag("Player").transform;

        if (controlScript.Difficulty == 1) //Changes attributes base on difficulty
        {
            startCooldown = 75;
            rotSpeed = 50;
            moveSpeed = -1.5f;
        }else if (controlScript.Difficulty == 3) 
        {
            startCooldown = 25;
        }else if (controlScript.Difficulty == 4) //Changes with Extremely Unfriendly: Instantly launches & can start to circle around the player
        {
            startCooldown = 0;
            rotSpeed = 120;
        }else if (controlScript.Difficulty == 5)
        {
            startCooldown = 0;
            rotSpeed = 150;
            moveSpeed = -2.2f;
        }else if (controlScript.Difficulty == 6)
        {
            startCooldown = 0;
            rotSpeed = 175;
            moveSpeed = -2.5f;
        }else if (controlScript.Difficulty == 0)
        {
            moveSpeed = 1;
        }
    }

    void FixedUpdate()
    {
        //Pre-launch behavior
        if (hasLaunched == false && transform.position.y > 1.05)
        {
            rb.velocity = transform.up * -preLaunchSpeed;
        }
        else if (hasLaunched == false && startCooldown > 0)
        {
            rb.velocity = Vector3.zero;
            startCooldown -= 1;
        }
        else { hasLaunched = true; }

        if (hasLaunched == true)
        {
            animTimer += 1;
            if (animTimer / animCooldown == Mathf.Round(animTimer / animCooldown)) //This frame change animation
            {
                if (animTimer == animCooldown)
                {
                    GetComponent<SpriteRenderer>().sprite = spin1;
                }else if (animTimer == animCooldown * 2)
                {
                    GetComponent<SpriteRenderer>().sprite = spin2;
                }else if (animTimer == animCooldown * 3)
                {
                    GetComponent<SpriteRenderer>().sprite = spin3;
                }else if (animTimer == animCooldown * 4)
                {
                    GetComponent<SpriteRenderer>().sprite = spin4;
                    animTimer = 0;
                }
            }

            dir = (Vector2)pObj.position - rb.position;
            dir.Normalize();
            rotAmount = Vector3.Cross(dir, transform.up).z;

            rb.angularVelocity = rotAmount * rotSpeed;
            if (controlScript.Difficulty < 4) //Limits rotation on lower difficulties
            {
                if (transform.eulerAngles.z > 30 && transform.eulerAngles.z < 330)
                {
                    rb.angularVelocity = 0;
                }
            }

            rb.velocity = transform.up * moveSpeed;
        }

        if (transform.position.y < -2) //Destroys zooper if off screen :(
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -2)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > 2)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (explosionObject != null)
        {
            GameObject newChild = Instantiate(explosionObject, transform.position, transform.rotation);
            Rigidbody2D childRb = newChild.GetComponent<Rigidbody2D>();
            childRb.velocity = rb.velocity;
            childRb.angularVelocity = rb.angularVelocity;
            childRb.rotation = rb.rotation;

            newChild.GetComponent<zooperExplosionScript>().moveSpeed = moveSpeed * 0.85f;
        }
    }
}