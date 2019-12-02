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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            Debug.Log(other.name);
        }
    }


}
