using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpriteController : MonoBehaviour
{
    float myRotation = 0f;

    void FixedUpdate()
    {
        //Friction
        if (!(Input.GetKey("left") || Input.GetKey("right")))
        {
            myRotation = myRotation * 0.90f;
        }

        //Rotation
        if (Input.GetKey("right"))
        {
            myRotation -= 0.1f;
        }
        if (Input.GetKey("left"))
        {
            myRotation += 0.1f;
        }

        transform.eulerAngles = new Vector3(0, 0, myRotation);

        //Limiting rotation
        if (myRotation < -10)
        {
            transform.eulerAngles = new Vector3(0, 0, -10);
        }
        if (myRotation > 10)
        {
            transform.eulerAngles = new Vector3(0, 0, 10);
        }
    }
}
