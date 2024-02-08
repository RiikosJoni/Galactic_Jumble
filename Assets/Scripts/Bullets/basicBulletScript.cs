using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class basicBulletScript : MonoBehaviour
{
    GameObject gC;
    gameController controlScript;

    public GameObject explosionObject;

    public float speed = 0.1f;

    private void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        speed = speed * (controlScript.Difficulty * 0.5f);
    }

    void FixedUpdate()
    {
        transform.position += transform.right * speed;

        if (transform.position.y < -2 || transform.position.y > 2 || transform.position.x < -2 || transform.position.x > 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (explosionObject != null)
        {
            Instantiate(explosionObject, transform.position, new Quaternion());
        }
    }
}
