using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBulletScript : MonoBehaviour
{
    GameObject gC;
    gameController controlScript;

    public GameObject explosionObject;

    void Start()
    {
        gC = GameObject.Find("GameController");
        controlScript = gC.GetComponent<gameController>();

        transform.position += new Vector3(0, -0.03f, 0.03f);
        transform.eulerAngles += new Vector3(0, 0, 180);
    }
    void FixedUpdate()
    {
        if (controlScript.isPaused == false)
        {
            if (transform.position.y < -2)
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
