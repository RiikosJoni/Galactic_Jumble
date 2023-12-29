using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class zooperEnemyScript : MonoBehaviour
{
    //This script handles the behaviors the Zooper, the best enemy in the game :)

    GameObject gC;
    gameController controlScript;

    public int startCooldown = 100; //The time between it reaching the launching height on the screen and launching at the player
    public float preLaunchSpeed = 0.1f; //How fast it moves before launching off
    [SerializeField] bool hasLaunched = false;

    public float moveSpeed = 0.1f; //How fast the enemy moves
    public float rotSpeed = 0.1f; //How fast the enemy rotates

    Vector2 dir;
    float rotAmount;

    public Sprite spin4, spin3, spin2, spin1; //Sprites the enemy uses for it's animation
    public int animCooldown = 100;

    Rigidbody2D rb;

    Transform pObj; //the PlayerObject
    float playerXPos;
    float playerYPos;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        rb = GetComponent<Rigidbody2D>();

        pObj = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        //Pre-launch behavior
        if (hasLaunched == false && transform.position.y > 1.05)
        {
            transform.position += new Vector3(0, -preLaunchSpeed, 0);
        }
        else if (hasLaunched == false && startCooldown > 0)
        {
            startCooldown -= 1;
        }
        else { hasLaunched = true; }

        if (hasLaunched == true)
        {
            dir = (Vector2)pObj.position - rb.position;
            dir.Normalize();
            rotAmount = Vector3.Cross(dir, transform.up).z;

            rb.angularVelocity = rotAmount * rotSpeed;
            rb.velocity = transform.up * moveSpeed;
        }

        if (transform.position.y < -2) //Destroys zooper if off screen :(
        {
            Destroy(gameObject);
        }
    }
}