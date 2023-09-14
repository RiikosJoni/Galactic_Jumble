using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControlScript : MonoBehaviour
{
    public GameObject Camera;
    public float shake = 0;
    public float shakeAmount = 0.01f;
    public float decreaseFactor = 3.0f;
 
    private void FixedUpdate()
    {
        if (shake > 0)
        {
            Camera.transform.localPosition = new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 0);
            shake -= 1 * decreaseFactor;
        }
        else
        {
            Camera.transform.localPosition = new Vector3(0, 0, 0);
            shake = 0;
        }
    }
}
