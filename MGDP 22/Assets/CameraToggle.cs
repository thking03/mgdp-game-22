using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public GameObject targetObject;
    private float targetAngle = 0;
    const float rotationAmount = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    //Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographic = true;
    }

    // Update is called once per frame
    void Update()
    {

        // Trigger functions if Rotate is requested
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //bc camera is initialized as orthographic, functions as an "if in initial position"
            //(might need to be adjusted if we change that initialization though)
            if (Camera.main.orthographic)
            {
                targetAngle -= 90.0f;
            }
            else
            {
                targetAngle += 90.0f;
            }
            //toggle!
            Camera.main.orthographic = !Camera.main.orthographic;
        }

        if (targetAngle != 0)
        {
            Rotate();
        }
    }

    protected void Rotate()
    {

        float step = rSpeed * Time.deltaTime;
        float orbitCircumfrance = 2F * rDistance * Mathf.PI;
        float distanceDegrees = (rSpeed / orbitCircumfrance) * 360;
        float distanceRadians = (rSpeed / orbitCircumfrance) * 2 * Mathf.PI;

        if (targetAngle > 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
}
