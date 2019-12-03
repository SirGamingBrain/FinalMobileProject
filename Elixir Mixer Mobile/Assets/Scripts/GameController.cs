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
    GameObject fade;

    GameObject introPanel;
    GameObject victoryPanel;
    GameObject failurePanel;
    GameObject guideBook;

    public CanvasGroup fadeAlpha;
    public CanvasGroup introAlpha;

    Transform timerHand;

    TextMeshProUGUI timerText;
    TextMeshProUGUI introText;
    TextMeshProUGUI dayText;
    TextMeshProUGUI endText;
    TextMeshProUGUI stats;

    float levelTimer = 300;

    int currentCash = 0;
    int minimumCash = 50;
    int totalProfit = 0;
    int maxPotionIndex = 8;
    int ordersFinished = 0;

    bool timerOn = false;
    bool dayFinished = false;
    bool fadeIn = false;
    bool fadeIntro = false;
    bool failed = false;

    bool[] customersWaiting = new bool[6] {false, false, false, false, false, false};
    public GameObject[] customers = new GameObject [5];
    public GameObject[] potionOrders = new GameObject[8];
    public Transform[] spawnpoint = new Transform[6];
    public Transform[] orderHolder = new Transform[6];

    AudioSource notifications;

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

        fade = GameObject.Find("Fade");
        introPanel = GameObject.Find("Intro Holder");

        introText = GameObject.Find("Intro Text").GetComponent<TextMeshProUGUI>();
        dayText = GameObject.Find("Shop Day").GetComponent<TextMeshProUGUI>();

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
                introText.text = "Good luck on your first day apprentice! If you need a moment to go over the recipes, feel free to do so now! Otherwise once you tap on the button on screen, the day will begin!";
                break;
            case "Day 2":
                blackeyedCrate.SetActive(false);
                magicCrate.SetActive(false);
                maxPotionIndex = 4;
                minimumCash = 60;
                introText.text = "Heads up, we got two new potions recipes ready for you to start crafting today! When you're ready, go ahead and tap on the button to open up shop!";
                break;
            case "Day 3":
                magicCrate.SetActive(false);
                maxPotionIndex = 5;
                minimumCash = 70;
                introText.text = "Another new potion has come in today, this time it's with an ingredient not even I have touched. Good luck working with and completing more orders today so we can eat!";
                break;
            case "Day 4":
                magicCrate.SetActive(false);
                maxPotionIndex = 6;
                minimumCash = 80;
                introText.text = "So I figured out there was a Stamina Potion we could make, but apparently word got out and now they want some of it. Do your best out there today!";
                break;
            case "Day 5":
                magicCrate.SetActive(false);
                maxPotionIndex = 7;
                minimumCash = 90;
                introText.text = "Customers have figured out that we've been holding back on all the potions we could make, so it's time we dish them out to them. We've got a new Perception Potion now, so it's time to reveal to the customers their true eyes!";
                break;
            case "Day 6":
                maxPotionIndex = 8;
                minimumCash = 100;
                introText.text = "Final day of your first work week my boy, and your this close to tasting huge success. Were breaking out all the stops for this day today, so have fun making the Revive Potion for rabid customers out there.";
                break;
            default:
                break;
        }

        dayText.text = PlayerPrefs.GetString("Day Number");
        Debug.Log("Day Number " + PlayerPrefs.GetString("Day Number"));

        //Fade in and begin the intro.
        //Once the intro is complete, the timer and the level for that day will begin.
        fadeIn = true;
        fade.SetActive(true);
        introPanel.SetActive(true);
        fadeAlpha.alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Fade in and out the scene.
        if (fadeIn && fade.activeInHierarchy)
        {
            if (fadeAlpha.alpha > 0f)
            {
                fadeAlpha.alpha -= Time.deltaTime;
            }
            else
            {
                fadeAlpha.alpha = 0f;
                fade.SetActive(false);
            }
        }
        else if (fadeIn == false && fade.activeInHierarchy)
        {
            if (fadeAlpha.alpha < 1f)
            {
                fadeAlpha.alpha += Time.deltaTime;
            }
            else
            {
                fadeAlpha.alpha = 1f;
                PlayerPrefs.Save();
                SceneManager.LoadScene("Chas UI");
            }
        }

        //Fade out the intro panel.
        if (fadeIntro && introPanel.activeInHierarchy)
        {
            if (introAlpha.alpha > 0f)
            {
                introAlpha.alpha -= Time.deltaTime;
            }
            else
            {
                introAlpha.alpha = 0f;
                introPanel.SetActive(false);
                timerOn = true;
                StartCoroutine(CustomerSpawning());
                //Can also play a starting sound here.
            }
        }

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
                //Play an ending sound here.
                FindObjectOfType<GameAudioScript>().Play("Day End", notifications);
                //Check to see if the player succeeded or failed.
                if (currentCash < minimumCash)
                {
                    failed = true;
                }
            }
        }

        if (dayFinished && !failed)
        {
            //Call a function to display the finishing stats to the player.
            //This will provide the player with a victory screen!
            //Finally they will have 3 options: Go to the next level, go back to the level select, or save & quit.
        }
        else
        {
            //Call a function to display the finishing stats to the player.
            //This will provide the player with a victory screen!
            //Finally they will have 3 options: Go to the next level, go back to the level select, or save & quit.
        }
    }

    public void StartLevel()
    {
        fadeIntro = true;
    }

    public void loadLevelSelect()
    {

    }

    public void nextDay()
    {

    }
    public void tryAgain()
    {

    }

    public void saveAndExit()
    {
        PlayerPrefs.Save();
        Application.Quit();
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
        ordersFinished += 1;
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
