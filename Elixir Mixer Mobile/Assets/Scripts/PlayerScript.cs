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

    float xSpeed = 0f;
    float zSpeed = 0f;
    float heading = 0f;
    float tempHeading = 0f;


    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        rig.velocity = new Vector3(joystick.Horizontal * 10, rig.velocity.y, joystick.Vertical * 10);
        //Debug.Log(joystick.Horizontal + "horizontal");
        //Debug.Log(joystick.Vertical + "vertical");

        if (joystick.Horizontal < 0)
        {
            Debug.Log("rotate left");
            Vector3 target = new Vector3(0, -90, 0);
            Vector3 newYrotate = Vector3.RotateTowards(-transform.right, target , 100f * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newYrotate);
        }
        if (joystick.Horizontal > 0)
        {
            Debug.Log("rotate right");
        }



    }


}
