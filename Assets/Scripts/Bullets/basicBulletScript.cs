using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicBulletScript : MonoBehaviour
{
    GameObject gC;
    gameController controlScript;

    public GameObject explosionObject;

    public float moveSpeed = 0.1f;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        transform.position += new Vector3(0, -0.05f, 0.03f);
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed;

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
