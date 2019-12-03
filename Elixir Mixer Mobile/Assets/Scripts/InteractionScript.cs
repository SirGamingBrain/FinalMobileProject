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
        Debug.Log("I'm entering a trigger HERE.");

        if (other.tag == "Blood")
        {
            interaction.text = "Grab Blood Rose";
        }
        else if (other.CompareTag("Dragon"))
        {
            interaction.text = "Grab Dragon Horn";
        }
        else if (other.CompareTag("Blooming"))
        {
            interaction.text = "Grab Blooming Moon";
        }
        else if (other.CompareTag("Cyclops"))
        {
            interaction.text = "Grab Cyclops Eye";
        }
        else if (other.CompareTag("Volcanic"))
        {
            interaction.text = "Grab Volcanic Ore";
        }
        else if (other.tag == ("Undead"))
        {
            interaction.text = "Grab Undead Soul";
        }
        else if (other.CompareTag("Blackeyed"))
        {
            interaction.text = "Grab Blackeyed Gold";
        }
        else if (other.CompareTag("Magic"))
        {
            interaction.text = "Grab Magic Mushroom";
        }

        if (other.name == "Furnace")
        {
            interaction.text = "Bake/Grab Ingredient";
        }
        else if (other.name == "Basket")
        {
            interaction.text = "Grind/Grab Ingredient";
        }
        else if (other.name == "Cauldron")
        {
            interaction.text = "Make/Grab Potion";
        }
        else if (other.name == "Cutting Board")
        {
            interaction.text = "Extract/Grab Ingredient";
        }
        else if (other.name == "bucket/Trash")
        {
            interaction.text = "Toss Inventory";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interaction.text = "";
    }
}
