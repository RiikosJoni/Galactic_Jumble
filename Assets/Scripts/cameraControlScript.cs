using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControlScript : MonoBehaviour
{
    public gameController gameController;
    public GameObject Camera;
    public float shake = 0;
    public float shakeAmount = 0.003f;
    public float decreaseFactor = 3.0f;
 
    private void FixedUpdate()
    {
        if (shake > 0)
        {
            Time.timeScale = 0.4f;
            Camera.transform.localPosition = new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 0);
            shake -= 1 * decreaseFactor;
        }
        else
        {
            if (Time.timeScale == 0.4f)
            {
                Time.timeScale = 1;
            }
            Camera.transform.localPosition = new Vector3(0, 0, 0);
            shake = 0;
        }
    }
}
