using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerBulletScript : MonoBehaviour
{
    GameObject gC;
    gameController controlScript;

    public GameObject explosionObject;

    public string bulletType = "Basic";

    public int bulletDamage = 1;
    float bulletSpeed = 1;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        transform.position += new Vector3(0, 0, 0.03f);
    }

    void FixedUpdate()
    {
        if (controlScript.isPaused == false)
        {
            if (bulletType == "Basic")
            {
                transform.position += new Vector3(0, bulletSpeed / 20, 0);
            }

            if (transform.position.y > 2)
            {
                Destroy(gameObject);
            }
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
