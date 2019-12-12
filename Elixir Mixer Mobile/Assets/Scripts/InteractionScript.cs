using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionScript : MonoBehaviour
{
    TextMeshProUGUI interaction;

    // Start is called before the first frame update
    void Start()
    {
        interaction = GameObject.Find("Interaction").GetComponent<TextMeshProUGUI>();

        interaction.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("I'm entering a trigger HERE.");

        if (other.name == "Blood Trigger")
        {
            interaction.text = "Grab Blood Rose";
        }
        else if (other.name == "Dragon Trigger")
        {
            interaction.text = "Grab Dragon Horn";
        }
        else if (other.name == "Blooming Trigger")
        {
            interaction.text = "Grab Blooming Moon";
        }
        else if (other.name == "Cyclops Trigger")
        {
            interaction.text = "Grab Cyclops Eye";
        }
        else if (other.name == "Volcanic Trigger")
        {
            interaction.text = "Grab Volcanic Ore";
        }
        else if (other.name == "Undead Trigger")
        {
            interaction.text = "Grab Undead Soul";
        }
        else if (other.name == "Blackeyed Trigger")
        {
            interaction.text = "Grab Blackeyed Gold";
        }
        else if (other.name == "Magic Trigger")
        {
            interaction.text = "Grab Magic Mushroom";
        }

        if (other.name == "Furnace")
        {
            interaction.text = "Bake Item";
        }
        else if (other.name == "Basket")
        {
            interaction.text = "Grind Item";
        }
        else if (other.name == "Cauldron")
        {
            interaction.text = "Use Cauldron";
        }
        else if (other.name == "Cutting Board")
        {
            interaction.text = "Extract Item";
        }
        else if (other.name == "bucket/Trash")
        {
            interaction.text = "Toss Item";
        }
        else if (other.tag == "Customer Order Zone")
        {
            interaction.text = "Turn in Item";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interaction.text = "";
    }
}
