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
    GameObject settingsPage;

    //GameObject guideBook //This will be done at a later date.

    public CanvasGroup fadeAlpha;
    public CanvasGroup introAlpha;

    Transform timerHand;

    TextMeshProUGUI timerText;
    TextMeshProUGUI introText;
    TextMeshProUGUI volumeText;
    TextMeshProUGUI dayText; //This is for the intro to tell you what day you are on.
    TextMeshProUGUI winText; //Win stats.
    TextMeshProUGUI failText; //Fail stats.

    float levelTimer = 300;

    int currentCash = 0;
    int minimumCash = 50;
    int ordersMissed = 0;
    int maxPotionIndex = 8;
    int ordersFinished = 0;

    bool timerOn = false;
    bool dayFinished = false;
    bool fadeIn = false;
    bool fadeIntro = false;
    bool failed = false;
    bool backToLevelSelect = false;
    bool nextLevel = false;
    bool reloadLevel = false;

    Toggle low;
    Toggle medium;
    Toggle high;

    Slider master;

    AudioSource alerts;
    public AudioSource clicks;
    public AudioSource orders;

    bool[] customersWaiting = new bool[6] {false, false, false, false, false, false};
    public GameObject[] customers = new GameObject [5]; //A gameobjects holder for the template of customers.
    GameObject[] currentCustomers = new GameObject[6]; //A list of customers currently out.
    public GameObject[] orderZones = new GameObject[6]; //The zones where the player turns in the order.
    public int[] customerOrders = new int[6]; //The spot that checks to see if you turned in the correct order for the customer.
    public GameObject[] potionOrders = new GameObject[8]; //A gameobjects holder for the template of orders.
    public Transform[] spawnpoint = new Transform[6]; //The spawnpoints for the customers.
    public Transform[] orderHolder = new Transform[6]; //The UI that holds the orders to display to the player.

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

        settingsPage = GameObject.Find("Settings");

        low = GameObject.Find("Low Quality Toggle").GetComponent<Toggle>();
        medium = GameObject.Find("Medium Quality Toggle").GetComponent<Toggle>();
        high = GameObject.Find("High Quality Toggle").GetComponent<Toggle>();

        master = GameObject.Find("Volume Slider").GetComponent<Slider>();
        volumeText = GameObject.Find("Volume Value").GetComponent<TextMeshProUGUI>();

        introText = GameObject.Find("Intro Text").GetComponent<TextMeshProUGUI>();
        dayText = GameObject.Find("Shop Day").GetComponent<TextMeshProUGUI>();

        timerHand = GameObject.Find("Clock Hand").GetComponent<Transform>();
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();

        winText = GameObject.Find("Win Stats").GetComponent<TextMeshProUGUI>();

        failText = GameObject.Find("Fail Stats").GetComponent<TextMeshProUGUI>();

        victoryPanel = GameObject.Find("Victory Screen");
        failurePanel = GameObject.Find("Failure Screen");

        alerts = this.GetComponent<AudioSource>();

        //Based on the day selected, need to hide certain objects, introduce new features, and explain any other changes.
        switch (PlayerPrefs.GetString("Day Number"))
        {
            case "Day 1":
                undeadCrate.SetActive(false);
                blackeyedCrate.SetActive(false);
                magicCrate.SetActive(false);
                maxPotionIndex = 3;
                minimumCash = 10;
                introText.text = "Good luck on your first day apprentice! If you need a moment to go over the recipes, feel free to do so now! Otherwise once you tap on the button on screen, the day will begin!";
                break;
            case "Day 2":
                blackeyedCrate.SetActive(false);
                magicCrate.SetActive(false);
                maxPotionIndex = 5;
                minimumCash = 20;
                introText.text = "Heads up, we got two new potions recipes ready for you to start crafting today! When you're ready, go ahead and tap on the button to open up shop!";
                break;
            case "Day 3":
                magicCrate.SetActive(false);
                maxPotionIndex = 6;
                minimumCash = 30;
                introText.text = "Another new potion has come in today, this time it's with an ingredient not even I have touched. Good luck working with and completing more orders today so we can eat!";
                break;
            case "Day 4":
                magicCrate.SetActive(false);
                maxPotionIndex = 7;
                minimumCash = 40;
                introText.text = "So I figured out there was a Stamina Potion we could make, but apparently word got out and now they want some of it. Do your best out there today!";
                break;
            case "Day 5":
                magicCrate.SetActive(false);
                maxPotionIndex = 8;
                minimumCash = 50;
                introText.text = "Customers have figured out that we've been holding back on all the potions we could make, so it's time we dish them out to them. We've got a new Perception Potion now, so it's time to reveal to the customers their true eyes!";
                break;
            case "Day 6":
                maxPotionIndex = 9;
                minimumCash = 60;
                introText.text = "Final day of your first work week my boy, and your this close to tasting huge success. Were breaking out all the stops for this day today, so have fun making the Revive Potion for rabid customers out there.";
                break;
            default:
                break;
        }

        switch (PlayerPrefs.GetInt("Quality"))
        {
            case 0:
                low.isOn = true;
                break;
            case 1:
                medium.isOn = true;
                break;
            case 2:
                high.isOn = true;
                break;
            default:
                high.isOn = true;
                break;
        }

        master.value = PlayerPrefs.GetFloat("Master Volume");
        volumeText.text = PlayerPrefs.GetFloat("Master Volume").ToString();

        dayText.text = PlayerPrefs.GetString("Day Number");
        Debug.Log("Day Number " + PlayerPrefs.GetString("Day Number"));

        victoryPanel.SetActive(false);
        failurePanel.SetActive(false);

        //Fade in and begin the intro.
        //Once the intro is complete, the timer and the level for that day will begin.
        fadeIn = true;
        fade.SetActive(true);
        settingsPage.SetActive(false);
        introPanel.SetActive(true);
        FindObjectOfType<GameAudioScript>().FadeIn();
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
                if (backToLevelSelect)
                {
                    PlayerPrefs.SetInt("From Level", 1);
                }
                else if (nextLevel)
                {
                    switch (PlayerPrefs.GetString("Day Number"))
                    {
                        case "Day 1":
                            PlayerPrefs.SetInt("Days Beaten", 1);
                            PlayerPrefs.SetString("Day Number", "Day 2");
                            break;
                        case "Day 2":
                            PlayerPrefs.SetInt("Days Beaten", 2);
                            PlayerPrefs.SetString("Day Number", "Day 3");
                            break;
                        case "Day 3":
                            PlayerPrefs.SetInt("Days Beaten", 3);
                            PlayerPrefs.SetString("Day Number", "Day 4");
                            break;
                        case "Day 4":
                            PlayerPrefs.SetInt("Days Beaten", 4);
                            PlayerPrefs.SetString("Day Number", "Day 5");
                            break;
                        case "Day 5":
                            PlayerPrefs.SetInt("Days Beaten", 5);
                            PlayerPrefs.SetString("Day Number", "Day 6");
                            break;
                        case "Day 6":
                            PlayerPrefs.SetInt("Days Beaten", 5);
                            PlayerPrefs.SetString("Day Number", "Day 6");
                            break;
                        default:
                            break;
                    }
                }

                fadeAlpha.alpha = 1f;
                PlayerPrefs.Save();

                if (backToLevelSelect)
                {
                    SceneManager.LoadScene("Main Menu");
                }
                else if (nextLevel)
                {
                    SceneManager.LoadScene("Level 1");
                }
                else if (reloadLevel)
                {
                    SceneManager.LoadScene("Level 1");
                }
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
                FindObjectOfType<GameAudioScript>().Play("Level Start", alerts);
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
                StopAllCoroutines();
                DeleteAllOrders();
                //Play an ending sound here based on if they won or lost.
                //Check to see if the player succeeded or failed.
                if (currentCash < minimumCash)
                {
                    failed = true;
                    FindObjectOfType<GameAudioScript>().Play("Day Fail", alerts);
                }
                else
                {
                    failed = false;
                    FindObjectOfType<GameAudioScript>().Play("Day Win", alerts);
                }
            }
        }

        if (dayFinished && !failed)
        {
            //Call a function to display the finishing stats to the player.
            victoryPanel.SetActive(true);
            winText.text = ((currentCash - minimumCash) + "$\n" + ordersFinished);
            FindObjectOfType<GameAudioScript>().Stop("Track 1");
            FindObjectOfType<GameAudioScript>().Stop("Track 2");
            //This will provide the player with a victory screen!
            //Finally they will have 3 options: Go to the next level, go back to the level select, or save & quit.
        }
        else if (dayFinished && failed)
        {
            //Call a function to display the finishing stats to the player.
            failurePanel.SetActive(true);
            failText.text = ((minimumCash - currentCash) + "$\n" + ordersFinished);
            FindObjectOfType<GameAudioScript>().Stop("Track 1");
            FindObjectOfType<GameAudioScript>().Stop("Track 2");
            //This will provide the player with a failure screen!
            //Finally they will have 3 options: Retry this level, go back to the level select, or save & quit.
        }
    }

    public void Settings()
    {
        Time.timeScale = 0;
        settingsPage.SetActive(true);
    }

    public void VolumeSlider(Slider slider)
    {
        PlayerPrefs.SetFloat("Master Volume", slider.value);
        volumeText.text = slider.value.ToString();
        FindObjectOfType<GameAudioScript>().VolumeChange();
    }

    public void ToggleHigh()
    {
        PlayerPrefs.SetInt("Quality", 2);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
    }

    public void ToggleMedium()
    {
        PlayerPrefs.SetInt("Quality", 1);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
    }

    public void ToggleLow()
    {
        PlayerPrefs.SetInt("Quality", 0);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
    }

    public void SaveAndBack()
    {
        PlayerPrefs.Save();
        settingsPage.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnTouch()
    {
        FindObjectOfType<GameAudioScript>().Play("Click", clicks);
    }

    public void checkOrder(int potion, GameObject customerArea)
    {
        if (customerArea == orderZones[0])
        {
            if (potion == customerOrders[0])
            {
                CompletedOrder(0);
            }
            else
            {
                FailedOrder(0);
            }
        }
        else if (customerArea == orderZones[1])
        {
            if (potion == customerOrders[1])
            {
                CompletedOrder(1);
            }
            else
            {
                FailedOrder(1);
            }
        }
        else if (customerArea == orderZones[2])
        {
            if (potion == customerOrders[2])
            {
                CompletedOrder(2);
            }
            else
            {
                FailedOrder(2);
            }
        }
        else if (customerArea == orderZones[3])
        {
            if (potion == customerOrders[3])
            {
                CompletedOrder(3);
            }
            else
            {
                FailedOrder(3);
            }
        }
        else if (customerArea == orderZones[4])
        {
            if (potion == customerOrders[4])
            {
                CompletedOrder(4);
            }
            else
            {
                FailedOrder(4);
            }
        }
        else if (customerArea == orderZones[5])
        {
            if (potion == customerOrders[5])
            {
                CompletedOrder(5);
            }
            else
            {
                FailedOrder(5);
            }
        }
        else
        {
            Debug.Log("Not sure how we ended up here, but we did.");
        }
    }

    public void StartLevel()
    {
        fadeIntro = true;
    }

    public void loadLevelSelect() //Button that loads the level select.
    {
        FindObjectOfType<GameAudioScript>().FadeOut();
        backToLevelSelect = true;
        fade.SetActive(true);
        fadeIn = false;
    }

    public void backtoMainMenu()
    {
        FindObjectOfType<GameAudioScript>().FadeOut();
        backToLevelSelect = true;
        fade.SetActive(true);
        fadeIn = false;
        Time.timeScale = 1;
    }

    public void nextDay() //Button to go to the next day.
    {
        FindObjectOfType<GameAudioScript>().FadeOut();
        nextLevel = true;
        fade.SetActive(true);
        fadeIn = false;
    }

    public void tryAgain() //Button to reset the current level.
    {
        FindObjectOfType<GameAudioScript>().FadeOut();
        reloadLevel = true;
        fade.SetActive(true);
        fadeIn = false;
    }

    public void saveAndExit() //Button that saves and closes the day.
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    void SpawnNewCustomer()
    {
        int i = Random.Range(0, 5); //Choose a random customer prefab.
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
        customerOrders[j] = k; 
        currentCustomers[j] = Instantiate(customers[i], spawnpoint[j].position, spawnpoint[j].rotation);
        Instantiate(potionOrders[k], orderHolder[j].position, orderHolder[j].rotation, orderHolder[j]);
    }

    public void FailedOrder(int waitingSpot)
    {
        Debug.Log("Failed an order!");
        FindObjectOfType<GameAudioScript>().Play("Failed Order", orders);
        //Clear the customer and the order and don't give the player anything.
        ordersMissed += 1;
        currentCustomers[waitingSpot].GetComponent<CustomerStatus>().destroyTime();
        orderHolder[waitingSpot].GetComponentInChildren<OrderTimer>().destroyTime();
        customersWaiting[waitingSpot] = false;
    }

    public void CompletedOrder(int waitingSpot)
    {
        Debug.Log("Completed an order and earned some cash!");
        FindObjectOfType<GameAudioScript>().Play("Completed Order", orders);
        //Clear the customer and the order and add cash to the player.
        currentCash += 5;
        ordersFinished += 1;
        currentCustomers[waitingSpot].GetComponent<CustomerStatus>().destroyTime();
        orderHolder[waitingSpot].GetComponentInChildren<OrderTimer>().destroyTime();
        customersWaiting[waitingSpot] = false;
    }

    public void DeleteAllOrders()
    {
        for (int i = 0; i < 6; i++)
        {
            if (currentCustomers[i] != null)
            {
                currentCustomers[i].GetComponent<CustomerStatus>().destroyTime();
            }

            if (orderHolder[i].transform.childCount > 0)
            {
                orderHolder[i].GetComponentInChildren<OrderTimer>().destroyTime();
            }
            
            customersWaiting[i] = false;
        }
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
