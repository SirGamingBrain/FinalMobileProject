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

    int currentCash = 0;
    int minimumCash = 50;
    int maxPotionIndex = 8;

    bool timerOn = false;
    bool dayFinished = false;

    bool[] customersWaiting = new bool[6] {false, false, false, false, false, false};
    public GameObject[] customers = new GameObject [5];
    public GameObject[] potionOrders = new GameObject[8];
    public Transform[] spawnpoint = new Transform[6];
    public Transform[] orderHolder = new Transform[6];
    //public Transform[] waitingPoint = new Transform[5];

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
                maxPotionIndex = 2;
                minimumCash = 50;
                break;
            case "Day 2":
                blackeyedCrate.SetActive(false);
                magicCrate.SetActive(false);
                maxPotionIndex = 4;
                minimumCash = 60;
                break;
            case "Day 3":
                magicCrate.SetActive(false);
                maxPotionIndex = 5;
                minimumCash = 70;
                break;
            case "Day 4":
                magicCrate.SetActive(false);
                maxPotionIndex = 6;
                minimumCash = 80;
                break;
            case "Day 5":
                magicCrate.SetActive(false);
                maxPotionIndex = 7;
                minimumCash = 90;
                break;
            case "Day 6":
                maxPotionIndex = 8;
                minimumCash = 100;
                break;
            default:
                break;
        }

        Debug.Log("Day Number " + PlayerPrefs.GetString("Day Number"));

        //Once the intro is complete, the day will begin.
        timerOn = true;
        StartCoroutine(CustomerSpawning());
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

    void SpawnNewCustomer()
    {
        int i = Random.Range(0, 4); //Choose a random customer prefab.
        int j = Random.Range(0, 5); //Choose a random waiting spot.
        int k = Random.Range(0, maxPotionIndex); //Choose a random order based on the day.

        //Check to see if that spot is being used, if it is we need to find one that isn't being used currently.
        if (customersWaiting[j])
        {
            bool empty = false;

            while (empty == false)
            {
                if (j < 5)
                {
                    j++;
                }
                else
                {
                    j = 0;
                }

                if (!customersWaiting[j])
                {
                    empty = true;
                }
            }
        }

        Debug.Log("Customer #" + i + ", in line " + j + " ordering potion number: " + k + ".");
        //Spawn the order and the customer in and begin timing them.
        customersWaiting[j] = true;
        Instantiate(customers[i], spawnpoint[j].position, spawnpoint[j].rotation);
        Instantiate(potionOrders[k], orderHolder[j].position, orderHolder[j].rotation, orderHolder[j]);
    }

    public void FailedOrder(int waitingSpot)
    {
        //Clear the customer and the order and don't give the player anything.
        customersWaiting[waitingSpot] = false;
    }

    public void CompletedOrder(int waitingSpot)
    {
        //Clear the customer and the order and add cash to the player.
        currentCash += 5;
        customersWaiting[waitingSpot] = false;
    }

    IEnumerator CustomerSpawning()
    {
        while (true)
        {
            int i = 0;

            foreach (bool spot in customersWaiting)
            {
                if (spot)
                {
                    i++;
                }
            }

            if (i == 6)
            {
                Debug.Log("Waiting for a new spot to be open.");
                yield return new WaitForSeconds(3);
            }
            else
            {
                Debug.Log("New spot found, beginning spawning!");
                yield return new WaitForSeconds(5);
                SpawnNewCustomer();
            }
        }
    }
}
