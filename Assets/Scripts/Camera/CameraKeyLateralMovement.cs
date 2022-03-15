/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Garon Ponder, Stephanie Simpson and Daniel Ta
Purpose: Our main method to move the camera around and see the scene.
Game Objects Associated: SampleScene; Main Camera
Files Associated: N/A
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyLateralMovement : MonoBehaviour
{
    public float forwardSpeed = 0.05f;
    public float strafeSpeed = 0.02f;
    public float riseSpeed = 0.05f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cammove();
    }

    void cammove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(0, 0, forwardSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(0, 0, -forwardSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(-strafeSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(strafeSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Camera.main.transform.Translate(0, -riseSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            Camera.main.transform.Translate(0, riseSpeed, 0);
        }
    }
}
