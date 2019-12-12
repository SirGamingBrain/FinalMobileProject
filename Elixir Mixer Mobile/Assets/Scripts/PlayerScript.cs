using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator playerAnimator;
    
    protected Joystick joystick;

    Rigidbody rig;

    public float moveSpeed = 5f;
    public float multiplier = 2f;

    static string tempName;
    string toolName;
    string ButtonName;
    static string interactName;

    public Image leftImage;
    public Image rightImage;

    public Sprite[] panels = new Sprite[34];

    int[] inventory = new int[2] {0,0};
    List<int> CauldronInventory = new List<int>();


    //List<string> Left = new List<string>();
    //List<string> Right = new List<string>();
    //List<string> furnace = new List <string>();
    //List<string> basket = new List<string>();
    //List<string> cutting = new List<string>();
    //List<string> cauldron = new List<string>();

    public Button lefthand;
    public Button righthand;
    

    //Text imageName;

    static bool interact = false;

    GameObject currentInteraction;


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

        if (moveVector != new Vector3(0f,0f,0f))
        {
            playerAnimator.SetBool("Running", true);
        }
        else
        {
            playerAnimator.SetBool("Running", false);
        }

        playerAnimator.SetBool("Carrying", false);

        foreach (int item in inventory)
        {
            if (item != 0)
            {
                playerAnimator.SetBool("Carrying", true);
            }
        }

        switch (inventory[0])
        {
            case 0:
                leftImage.sprite = panels[0];
               //Debug.Log("Left hand empty!");
                break;
            case 1:
                leftImage.sprite = panels[1];
                break;
            case 2:
                leftImage.sprite = panels[2];
                break;
            case 3:
                leftImage.sprite = panels[3];
                break;
            case 4:
                leftImage.sprite = panels[4];
                break;
            case 5:
                leftImage.sprite = panels[5];
                break;
            case 6:
                leftImage.sprite = panels[6];
                break;
            case 7:
                leftImage.sprite = panels[7];
                break;
            case 8:
                leftImage.sprite = panels[8];
                break;
            case 9:
                leftImage.sprite = panels[9];
                break;
            case 10:
                leftImage.sprite = panels[10];
                break;
            case 11:
                leftImage.sprite = panels[11];
                break;
            case 12:
                leftImage.sprite = panels[12];
                break;
            case 13:
                leftImage.sprite = panels[13];
                break;
            case 14:
                leftImage.sprite = panels[14];
                break;
            case 15:
                leftImage.sprite = panels[15];
                break;
            case 16:
                leftImage.sprite = panels[16];
                break;
            case 17:
                leftImage.sprite = panels[17];
                break;
            case 18:
                leftImage.sprite = panels[18];
                break;
            case 19:
                leftImage.sprite = panels[19];
                break;
            case 20:
                leftImage.sprite = panels[20];
                break;
            case 21:
                leftImage.sprite = panels[21];
                break;
            case 22:
                leftImage.sprite = panels[22];
                break;
            case 23:
                leftImage.sprite = panels[23];
                break;
            case 24:
                leftImage.sprite = panels[24];
                break;
            case 25:
                leftImage.sprite = panels[25];
                break;
            case 26:
                leftImage.sprite = panels[26];
                break;
            case 27:
                leftImage.sprite = panels[27];
                break;
            case 28:
                leftImage.sprite = panels[28];
                break;
            case 29:
                leftImage.sprite = panels[29];
                break;
            case 30:
                leftImage.sprite = panels[30];
                break;
            case 31:
                leftImage.sprite = panels[31];
                break;
            case 32:
                leftImage.sprite = panels[32];
                break;
            case 33:
                leftImage.sprite = panels[33];
                break;
            case 34:
                leftImage.sprite = panels[34];
                break;
            default:
                leftImage.sprite = panels[0];
                break;
        }

        switch (inventory[1])
        {
            case 0:
                rightImage.sprite = panels[0];
                break;
            case 1:
                rightImage.sprite = panels[1];
                break;
            case 2:
                rightImage.sprite = panels[2];
                break;
            case 3:
                rightImage.sprite = panels[3];
                break;
            case 4:
                rightImage.sprite = panels[4];
                break;
            case 5:
                rightImage.sprite = panels[5];
                break;
            case 6:
                rightImage.sprite = panels[6];
                break;
            case 7:
                rightImage.sprite = panels[7];
                break;
            case 8:
                rightImage.sprite = panels[8];
                break;
            case 9:
                rightImage.sprite = panels[9];
                break;
            case 10:
                rightImage.sprite = panels[10];
                break;
            case 11:
                rightImage.sprite = panels[11];
                break;
            case 12:
                rightImage.sprite = panels[12];
                break;
            case 13:
                rightImage.sprite = panels[13];
                break;
            case 14:
                rightImage.sprite = panels[14];
                break;
            case 15:
                rightImage.sprite = panels[15];
                break;
            case 16:
                rightImage.sprite = panels[16];
                break;
            case 17:
                rightImage.sprite = panels[17];
                break;
            case 18:
                rightImage.sprite = panels[18];
                break;
            case 19:
                rightImage.sprite = panels[19];
                break;
            case 20:
                rightImage.sprite = panels[20];
                break;
            case 21:
                rightImage.sprite = panels[21];
                break;
            case 22:
                rightImage.sprite = panels[22];
                break;
            case 23:
                rightImage.sprite = panels[23];
                break;
            case 24:
                rightImage.sprite = panels[24];
                break;
            case 25:
                rightImage.sprite = panels[25];
                break;
            case 26:
                rightImage.sprite = panels[26];
                break;
            case 27:
                rightImage.sprite = panels[27];
                break;
            case 28:
                rightImage.sprite = panels[28];
                break;
            case 29:
                rightImage.sprite = panels[29];
                break;
            case 30:
                rightImage.sprite = panels[30];
                break;
            case 31:
                rightImage.sprite = panels[31];
                break;
            case 32:
                rightImage.sprite = panels[32];
                break;
            case 33:
                rightImage.sprite = panels[33];
                break;
            case 34:
                rightImage.sprite = panels[34];
                break;
            default:
                rightImage.sprite = panels[0];
                break;
        }

        playerAnimator.ResetTrigger("Taking");
        playerAnimator.ResetTrigger("Giving");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            Debug.Log("You have opened the " + other.GetComponentInChildren<Transform>().transform.parent.name + "." + " Do you want to pick it up?");
            tempName = other.GetComponentInChildren<Transform>().transform.parent.name;
            
            //Debug.Log("Pick which hand you want to pick up the object with");
            //Debug.Log(lefthand.name);
        }

        //else if (other.name == "Furnace")
        //{
        //    interactName = other.name;
        //    Debug.Log("Furnace Call");
        //    toolName = other.name;
        //    FurnaceInteractions();

        //}

        //else if (other.name == "Basket")
        //{
        //    interactName = other.name;
        //    Debug.Log("basket Call");
        //    toolName = other.name;
        //}

        //else if (other.name == "bucket/Trash")
        //{
        //    interactName = other.name;
        //    Debug.Log("Trash Call");
        //    toolName = other.name;
        //    lefthand.onClick.AddListener(Ldrop);
        //    righthand.onClick.AddListener(Rdrop);
        //    lefthand.onClick.RemoveListener(LeftButton);
        //    righthand.onClick.RemoveListener(RightButton);

        //}

        //else if (other.name == "Cauldron")
        //{
        //    interactName = other.name;
        //    Debug.Log("Cauldron Call");
        //    toolName = other.name;
        //}

        //else if (other.name == "Cutting Board")
        //{
        //    interactName = other.name;
        //    Debug.Log("Cutting Board Call");
        //    toolName = other.name;
        //}

       
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            interactName = other.name;
            if (other.name == "Furnace")
            {
                currentInteraction = other.gameObject;
            }
            else if (other.name == "Basket")
            {
                currentInteraction = other.gameObject;
            }
            else if (other.name == "Cutting Board")
            {
                currentInteraction = other.gameObject;
            }
            else if (other.name == "Cauldron")
            {
                currentInteraction = other.gameObject;
            }
            else if (other.name == "bucket/Trash")
            {
                currentInteraction = other.gameObject;
            }
            else if (other.name == "Customer Order Zone")
            {
                currentInteraction = other.gameObject;
            }
            else
            {
                interactName = "Ingredient";
            }
            tempName = other.name;
            interact = true;
            //Debug.Log(interact + ", " + tempName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            interactName = null;
            interact = false;
            Debug.Log("Left an interaction zone!");
        }
    }

    int trash()
    {
        Debug.Log("its out");
        playerAnimator.SetTrigger("Giving");
        return 0; 
    }

    //Bake
    int FurnaceInteractions(int hand, int item)
    {
        int i = 0;
        switch (item)
        {
            case 0:
                i = item;
                Debug.Log("Empty Hand");
                break;
            case 2:
                i = 9;
                playerAnimator.SetTrigger("Giving");
                break;
            case 3:
                i = 10;
                //Debug.Log("grabing blood");
                playerAnimator.SetTrigger("Giving");
                break;
            case 4:
                i = 11;
                playerAnimator.SetTrigger("Giving");
                break;
            case 5:
                i = 12;
                playerAnimator.SetTrigger("Giving");
                break;
            case 6:
                i = 13;
                playerAnimator.SetTrigger("Giving");
                break;
            case 8:
                i = 14;
                playerAnimator.SetTrigger("Giving");
                break;
           
            default:
                i = item;
                Debug.Log("Invalid Item");
                break;
        }
        Debug.Log("grabbed item of value" + i + " in the " + hand);
        return i;
    }

    //Grind
    int BasketInteractions(int hand, int item)
    {
        int i = 0;
        switch (item)
        {
            case 0:
                i = item;
                Debug.Log("Empty Hand");
                break;
            case 2:
                i = 15;
                playerAnimator.SetTrigger("Giving");
                break;
            case 3:
                i =16;
                Debug.Log("grabing blood");
                playerAnimator.SetTrigger("Giving");
                break;
            case 4:
                i = 17;
                playerAnimator.SetTrigger("Giving");
                break;
            case 5:
                i = 18;
                playerAnimator.SetTrigger("Giving");
                break;
            case 7:
                i = 19;
                playerAnimator.SetTrigger("Giving");
                break;
            case 8:
                i = 20;
                playerAnimator.SetTrigger("Giving");
                break;
            default:
                i = item;
                Debug.Log("Invalid Item");
                break;
        }
        Debug.Log("grabbed item of value" + i + " in the " + hand);
        return i;


    }

    //Extract
    int CuttingBoardInterations(int hand, int item)
    {
        int i = 0;
        switch (item)
        {
            case 0:
                i = item;
                Debug.Log("Empty Hand");
                break;
            case 1:
                i = 21;
                playerAnimator.SetTrigger("Giving");
                break;
            case 4:
                i = 22;
                Debug.Log("grabing blood");
                playerAnimator.SetTrigger("Giving");
                break;
            case 6:
                i = 23;
                playerAnimator.SetTrigger("Giving");
                break;
            case 8:
                i = 24;
                playerAnimator.SetTrigger("Giving");
                break;  
            default:
                i = item;
                Debug.Log("Invalid Item");
                break;
        }
        Debug.Log("grabbed item of value" + i + " in the " + hand);
        return i;


    }

    //Potion
    int CauldronInteractions(int hand, int item)
    {
        int i = 0;
        int size = 0;
        switch (item)
        {
            case 0:
                size = CauldronInventory.Count;
                Debug.Log(size);
                switch (size)
                {
                    case 0:
                        Debug.Log("no potion to make");
                        break;
                    case 1:
                        Debug.Log("Potion crafting failed");
                        break;
                    case 2:
                        if (CauldronInventory.Contains(12) && CauldronInventory.Contains(15))
                        {
                            Debug.Log("Health Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 25;
                        }

                        else if (CauldronInventory.Contains(10) && CauldronInventory.Contains(17))
                        {
                            Debug.Log("Mana Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 26;
                        }
                        else if (CauldronInventory.Contains(18) && CauldronInventory.Contains(13))
                        {
                            Debug.Log("Strength Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 27;
                        }
                        else if (CauldronInventory.Contains(23) && CauldronInventory.Contains(16))
                        {
                            Debug.Log("Jumping Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 28;
                        }
                        else if (CauldronInventory.Contains(12) && CauldronInventory.Contains(14))
                        {
                            Debug.Log("Poision Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 29;
                        }
                        else if (CauldronInventory.Contains(24) && CauldronInventory.Contains(21))
                        {
                            Debug.Log("Speed Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 30;
                        }

                        else
                        {
                            Debug.Log("Nope");
                        }
                        break;
                    case 3:
                        if (CauldronInventory.Contains(23) && CauldronInventory.Contains(12) && CauldronInventory.Contains(24))
                        {
                            Debug.Log("Stamina Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 31;
                        }
                        else if (CauldronInventory.Contains(22) && CauldronInventory.Contains(11) && CauldronInventory.Contains(17))
                        {
                            Debug.Log("Perception Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 32;
                        }
                       
                        else
                        {
                            Debug.Log("Nope");
                        }
                        break;
                    case 4:
                        if (CauldronInventory.Contains(20) && CauldronInventory.Contains(9) && CauldronInventory.Contains(23) && CauldronInventory.Contains(19))
                        {
                            Debug.Log("Revive Potion");
                            playerAnimator.SetTrigger("Taking");
                            CauldronInventory.Clear();
                            return 33;
                        }
                        else
                        {
                            Debug.Log("Nope");
                        }
                        break;
                }
                CauldronInventory.Clear();
                Debug.Log("Cauldron is Clear");
                break;
            case 9:
                i = 9;
                break;
            case 10:
                i = 10;
                Debug.Log("grabing blood");
                break;
            case 11:
                i = 11;
                break;
            case 12:
                i = 12;
                break;
            case 13:
                i = 13;
                break;
            case 14:
                i = 14;
                break;
            case 15:
                i = 15;
                break;
            case 16:
                i = 16;
                break;
            case 17:
                i = 17;
                break;
            case 18:
                i = 18;
                break;
            case 19:
                i = 19;
                break;
            case 20:
                i = 20;
                break;
            case 21:
                i = 21;
                break;
            case 22:
                i = 22;
                break;
            case 23:
                i = 23;
                break;
            case 24:
                i = 24;
                break;

            default:
                
                Debug.Log("Invalid Item");
                for (int j = 0; j < CauldronInventory.Count; j++)
                {
                    j = 0;
                }
                break;
        }
        // Debug.Log("grabbed item of value" + i + " in the " + hand);

        if (CauldronInventory.Count == 4)
        {
            Debug.Log("Cauldron is full");
            return item;
        }
        else
        {
            CauldronInventory.Add(i);
            playerAnimator.SetTrigger("Giving");
            return 0;
        }
    }

    int GivePotion(int hand, int item)
    {
        int i = 0;
        int j = 0;

        switch (item)
        {
            case 0:
                Debug.Log("Empty Hand");
                i = item;
                break;
            case 25:
                i = 0;
                j = 0;
                playerAnimator.SetTrigger("Giving");
                break;
            case 26:
                i = 0;
                j = 1;
                playerAnimator.SetTrigger("Giving");
                break;
            case 27:
                i = 0;
                j = 2;
                playerAnimator.SetTrigger("Giving");
                break;
            case 28:
                i = 0;
                j = 3;
                playerAnimator.SetTrigger("Giving");
                break;
            case 29:
                i = 0;
                j = 4;
                playerAnimator.SetTrigger("Giving");
                break;
            case 30:
                i = 0;
                j = 5;
                playerAnimator.SetTrigger("Giving");
                break;
            case 31:
                i = 0;
                j = 6;
                playerAnimator.SetTrigger("Giving");
                break;
            case 32:
                i = 0;
                j = 7;
                playerAnimator.SetTrigger("Giving");
                break;
            case 33:
                i = 0;
                j = 8;
                playerAnimator.SetTrigger("Giving");
                break;
            default:
                i = 0;
                j = 9;
                playerAnimator.SetTrigger("Giving");
                Debug.Log("Wrong Item loser");
                break;
        }

        Debug.Log("Giving potion of value " + j + "to customer.");
        FindObjectOfType<GameController>().checkOrder(j, currentInteraction);

        //Debug.Log("grabbed item of value" + i + " in the " + hand);
        return i;
    }

    public void leftpressed()
    {
        Debug.Log("left button pressed");
        Debug.Log(interact);
        if (interact)
        {
            Debug.Log(interactName);
            switch (interactName)
            {
                case "Furnace":
                    inventory[0] = FurnaceInteractions(0, inventory[0]);
                    break;
                case "Basket":
                    inventory[0] = BasketInteractions(0, inventory[0]);
                    break;
                case "Cutting Board":
                    inventory[0] = CuttingBoardInterations(0, inventory[0]);
                    break;
                case "bucket/Trash":
                    inventory[0] = trash();
                    break;
                case "Cauldron":
                    inventory[0] = CauldronInteractions(0, inventory[0]);
                    break;
                case "Ingredient":
                    Debug.Log("Grabbing an ingredient!");
                    inventory[0] = grabIngredient(0, inventory[0]);
                    break;
                case "Customer Order Zone":
                    inventory[0] = GivePotion(0, inventory[0]);
                    break;
                 default:
                    break;
            }
        }
    }

   public void rightPressed()
    {
        Debug.Log("right button pressed");
        Debug.Log(interact);
        if (interact)
        {
            Debug.Log(interactName);
            switch (interactName)
            {
                case "Furnace":
                    inventory[1] = FurnaceInteractions(1, inventory[1]);
                    break;
                case "Basket":
                    inventory[1] = BasketInteractions(1, inventory[1]);
                    break;
                case "Cutting Board":
                    inventory[1] = CuttingBoardInterations(1, inventory[1]);
                    break;
                case "bucket/Trash":
                    inventory[1] = trash();
                    break;
                case "Cauldron":
                    inventory[1] = CauldronInteractions(1, inventory[1]);
                    break;
                case "Ingredient":
                    Debug.Log("Grabbing an ingredient!");
                    inventory[1] = grabIngredient(1, inventory[1]);
                    break;
                case "Customer Order Zone":
                    inventory[1] = GivePotion(1, inventory[1]);
                    break;
                default:
                    break;
            }
        }
    }

    int grabIngredient(int hand, int item)
    {
        int i = 0;
        if (item == 0)
        {
            Debug.Log(tempName);
            //inventory[0] = 0;
            switch (tempName)
            {
                case "Blackeyed Trigger":
                    i = 1;
                    break;
                case "Blood Trigger":
                    i = 2;
                    Debug.Log("grabing blood");
                    break;
                case "Blooming Trigger":
                    i =3;
                    break;
                case "Cyclops Trigger":
                    i= 4;
                    break;
                case "Dragon Trigger":
                    i = 5;
                    break;
                case "Volcanic Trigger":
                    i = 6;
                    break;
                case "Magic Trigger":
                     i = 7;
                    break;
                case "Undead Trigger":
                    i = 8;
                    break;
                default:
                    break;
            }
            Debug.Log("Grabbed item of value" + i + " in the " + hand + " hand value.");
            playerAnimator.SetTrigger("Taking");
            return i;
        }
        Debug.Log("Hand has item" + item + " in it");
        return hand;
       
    }
}
