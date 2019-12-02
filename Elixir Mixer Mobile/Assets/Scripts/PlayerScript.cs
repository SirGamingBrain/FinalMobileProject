using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    protected Joystick joystick;

    Rigidbody rig;

    bool moving = false;

    float angle;
    float xSpeed = 0f;
    float zSpeed = 0f;
    float heading = 0f;
    float tempHeading = 0f;
    float dCenter = 0f;

    Vector3 forwardVel;
    Vector3 horizontalVel;


    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //rig.velocity = new Vector3(joystick.Horizontal * 1, rig.velocity.y, joystick.Vertical * 1);
        //Debug.Log(joystick.Horizontal + "horizontal");
        //Debug.Log(joystick.Vertical + "vertical");

        transform.LookAt(new Vector3(0f, 2.5f, 0f));
        dCenter = Vector3.Distance(new Vector3(0, 1.5f, 0f), transform.position);
        if ((joystick.Horizontal == 0 && joystick.Vertical == 0))
        {
            moving = false;
           
        }
        else
        {
            moving = true;
           
        }

        //Updating the player's speed and direction of movement if they are moving, otherwise slow them overtime.
        if (moving == true)
        {
            xSpeed = joystick.Horizontal * 8f;
            zSpeed = joystick.Vertical * 8f;

            /*if (dCenter <= 2f)
            {
                zSpeed = 0f;
            }*/
        }
        else
        {
            if (xSpeed > 0.1f)
            {
                xSpeed -= .2f;
            }
            else if (xSpeed < -0.1f)
            {
                xSpeed += .2f;
            }
            else
            {
                xSpeed = 0f;
            }

            if (zSpeed > 0f)
            {
                zSpeed -= .2f;
            }
            else if (zSpeed < -0.2f)
            {
                zSpeed += .1f;
            }
            else
            {
                zSpeed = 0f;
            }
        }

        forwardVel = transform.forward * zSpeed;
        horizontalVel = transform.right * xSpeed;

        //rb.velocity = new Vector3(xSpeed, 0f,zSpeed);
        rig.velocity = (forwardVel + horizontalVel);

        //Updating the player's direction based on movement.
        if (moving)
        {
            heading = Mathf.Atan2(joystick.Horizontal, joystick.Vertical);
            tempHeading = heading;
        }

        transform.Rotate(0f, tempHeading * Mathf.Rad2Deg, 0f, Space.Self);


    }


}
