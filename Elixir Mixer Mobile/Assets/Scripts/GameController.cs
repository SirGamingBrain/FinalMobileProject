using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    GameObject bloodCrate;
    GameObject dragonCrate;
    GameObject bloomingCrate;
    GameObject cyclopsCrate;
    GameObject volcanicCrate;
    GameObject undeadCrate;
    GameObject blackeyedCrate;
    GameObject magicCrate;

    Transform timerHand;

    TextMeshProUGUI timerText;

    float levelTimer = 300;

    int minimumScore = 50;

    bool timerOn = false;
    bool dayFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        //Find objects we need to find.
        bloodCrate = GameObject.Find("Blood Rose Crate");
        dragonCrate = GameObject.Find("Dragon Horn Crate");
        bloomingCrate = GameObject.Find("Blooming Moon Crate");
        cyclopsCrate = GameObject.Find("Cyclops Eye Crate");
        volcanicCrate = GameObject.Find("Volcanic Ore Crate");
        undeadCrate = GameObject.Find("Undead Soul Crate");
        blackeyedCrate = GameObject.Find("Blackeyed Gold Crate");
        magicCrate = GameObject.Find("Magic Mushrooms Crate");

        timerHand = GameObject.Find("Clock Hand").GetComponent<Transform>();
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        //Based on the day selected, need to hide certain objects, introduce new features, and explain any other changes.
        switch (PlayerPrefs.GetString("Day Number"))
        {
            case "Day 1":
                undeadCrate.SetActive(false);
                blackeyedCrate.SetActive(false);
                magicCrate.SetActive(false);
                break;
            case "Day 2":
                blackeyedCrate.SetActive(false);
                magicCrate.SetActive(false);
                break;
            case "Day 3":
                magicCrate.SetActive(false);
                break;
            case "Day 4":
                magicCrate.SetActive(false);
                break;
            case "Day 5":
                magicCrate.SetActive(false);
                break;
            case "Day 6":
                break;
            default:
                break;
        }

        timerOn = true;

        //Once the intro is complete, the day will begin.
    }

    // Update is called once per frame
    void Update()
    {
        //Timer runs until the day is over, at which point we need an end screen.
        if (timerOn)
        {
            //Update the rotation of the clock hand.
            levelTimer -= Time.deltaTime;
            float clockRotation = levelTimer / 300f;
            timerHand.eulerAngles = new Vector3(0, 0, clockRotation * 360f);

            //Update the text on the timer.
            if (levelTimer > 200f && levelTimer <= 300f)
            {
                if (levelTimer > 280f)
                {
                    timerText.text = "6:00 AM";
                }
                else if (levelTimer > 260f)
                {
                    timerText.text = "7:00 AM";
                }
                else if (levelTimer > 240f)
                {
                    timerText.text = "8:00 AM";
                }
                else if (levelTimer > 220f)
                {
                    timerText.text = "9:00 AM";
                }
                else if (levelTimer > 200f)
                {
                    timerText.text = "10:00 AM";
                }
            }
            else if (levelTimer > 100f && levelTimer <= 200f)
            {
                if (levelTimer > 180f)
                {
                    timerText.text = "11:00 AM";
                }
                else if (levelTimer > 160f)
                {
                    timerText.text = "12:00 PM";
                }
                else if (levelTimer > 140f)
                {
                    timerText.text = "1:00 PM";
                }
                else if (levelTimer > 120f)
                {
                    timerText.text = "2:00 PM";
                }
                else if (levelTimer > 100f)
                {
                    timerText.text = "3:00 PM";
                }
            }
            else if (levelTimer > 0f && levelTimer <= 100f)
            {
                if (levelTimer > 80f)
                {
                    timerText.text = "4:00 PM";
                }
                else if (levelTimer > 60f)
                {
                    timerText.text = "5:00 PM";
                }
                else if (levelTimer > 40f)
                {
                    timerText.text = "6:00 PM";
                }
                else if (levelTimer > 20f)
                {
                    timerText.text = "7:00 PM";
                }
                else if (levelTimer > 0f)
                {
                    timerText.text = "8:00 PM";
                }
            }
            else
            {
                timerText.text = "9:00 PM";
            }


            //Handle when the level completes.
            if (levelTimer <= 0f)
            {
                timerText.text = "9:00 PM";
                levelTimer = 0f;
                dayFinished = true;
                timerHand.eulerAngles = new Vector3(0, 0, 0f);
                timerOn = false;
            }
        }

        if (dayFinished)
        {
            //Call a function to display the finishing stats to the player.
            //This will provide the player with knowing if they met the goal or not, and access to letting them retry if they failed.
        }
    }
}
