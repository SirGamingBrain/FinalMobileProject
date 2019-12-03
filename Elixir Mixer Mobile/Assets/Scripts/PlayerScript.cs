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
    List<string> Left = new List<string>();
    List<string> Right = new List<string>();

    public Button lefthand;
    public Button righthand;

    Text imageName;


    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rig = this.GetComponent<Rigidbody>();
        lefthand.GetComponent<Button>().onClick.AddListener(() => LeftButton());
        righthand.GetComponent<Button>().onClick.AddListener(() => RightButton());
       
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
            Debug.Log("You have opened the " + other.GetComponentInChildren<Transform>().transform.parent.name + "." + " Do you want to pick it up?");
            tempName = other.GetComponentInChildren<Transform>().transform.parent.name;
            Debug.Log("Pick which hand you want to pick up the object with");
        }

        if (other.name == "Furnace")
        {
            Debug.Log("Furnace Call");
        }

        if (other.name == "Basket")
        {
            Debug.Log("basket Call");
        }

        if (other.name == "bucket/Trash")
        {
            Debug.Log("Trash Call");
            lefthand.GetComponent<Button>().onClick.AddListener(() => drop());
        }

        if (other.name == "Cauldron")
        {
            Debug.Log("Cauldron Call");
        }

        if (other.name == "Cutting Board")
        {
            Debug.Log("Cutting Board Call");
        }
    }

    void pickup()
    {
        
    }

   public void LeftButton()
    {
        Debug.Log("Left Button");
        Left.Add(tempName);
        tempName = "";
        Debug.Log(Left[0]);
        if (Left.Count > 1)
        {
            Debug.Log("Hand is full");
        }
    }
    public void RightButton()
    {
        Debug.Log("Right Button");
        Right.Add(tempName);
        tempName = "";
        Debug.Log(Right[0]);
        if (Right.Count > 1)
        {
            Debug.Log("Hand is full");
        }
    }

    public void drop()
    {
        //if the left button has been clicked while someting is in the left hand list, drop that item and remove it from the list
        if (Left.Count == 1)
        {
            Debug.Log("you can place item into ");
        }
    }


    void kitchenInteractions()
    {

    }

}
