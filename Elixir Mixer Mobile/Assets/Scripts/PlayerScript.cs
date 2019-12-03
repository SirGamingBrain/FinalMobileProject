using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    protected Joystick joystick;

    Rigidbody rig;

    public float moveSpeed = 5f;
    public float multiplier = 2f;

    string tempName;
    List<string> LeftHand = new List<string>();
    List<string> RightHand = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0f, CrossPlatformInputManager.GetAxis("Vertical")) * moveSpeed;
        transform.LookAt(transform.position + moveVector);
        rig.AddForce(moveVector);
        pickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            Debug.Log("You have opened the " + other.GetComponentInChildren<Transform>().transform.parent.name + "." + " Do you want to pick it up?");
            tempName = other.GetComponentInChildren<Transform>().transform.parent.name;
        }
    }

    void pickup()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LeftHand.Add(tempName);
            Debug.Log("You have picked up that item");
            tempName = "";
            LeftHand.ForEach(Debug.Log);
            Debug.Log(tempName + "string dude");
        }
       

        else if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("You did not pick up that item");
        }
    }


}
