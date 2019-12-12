using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    GameObject mainPage;
    GameObject settingsPage;
    GameObject levelPage;
    GameObject fade;

    public CanvasGroup fadeAlpha;

    Button day1;
    Button day2;
    Button day3;
    Button day4;
    Button day5;
    Button day6;

    Toggle low;
    Toggle medium;
    Toggle high;

    TextMeshProUGUI volumeText;

    Slider master;

    bool fadeIn = false;

    private void Awake()
    {
        //Handle Setting Audio and Quality here as well.
        if (!PlayerPrefs.HasKey("Master Volume"))
        {
            PlayerPrefs.SetFloat("Master Volume", 100f);
        }

        if (!PlayerPrefs.HasKey("Quality"))
        {
            PlayerPrefs.SetInt("Quality", 2);
            QualitySettings.SetQualityLevel(2, true);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
        }

        if (!PlayerPrefs.HasKey("Days Beaten"))
        {
            PlayerPrefs.SetInt("Days Beaten", 0);
        }

        if (!PlayerPrefs.HasKey("From Level"))
        {
            PlayerPrefs.SetInt("From Level", 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Days Beaten", 5);
        //Find all of our menu variables and set them.
        mainPage = GameObject.Find("Main Panel");
        settingsPage = GameObject.Find("Settings");
        levelPage = GameObject.Find("Level Select");
        fade = GameObject.Find("Fade");

        day1 = GameObject.Find("Day 1").GetComponent<Button>();
        day2 = GameObject.Find("Day 2").GetComponent<Button>();
        day3 = GameObject.Find("Day 3").GetComponent<Button>();
        day4 = GameObject.Find("Day 4").GetComponent<Button>();
        day5 = GameObject.Find("Day 5").GetComponent<Button>();
        day6 = GameObject.Find("Day 6").GetComponent<Button>();

        low = GameObject.Find("Low Quality Toggle").GetComponent<Toggle>();
        medium = GameObject.Find("Medium Quality Toggle").GetComponent<Toggle>();
        high = GameObject.Find("High Quality Toggle").GetComponent<Toggle>();

        master = GameObject.Find("Volume Slider").GetComponent<Slider>();
        volumeText = GameObject.Find("Volume Value").GetComponent<TextMeshProUGUI>();

        //Handle Locking Level Select Buttons, Toggling Qualities, and Volume Slider here.
        Debug.Log("Days Beaten: " + PlayerPrefs.GetInt("Days Beaten").ToString() + ", Quality Index: " + PlayerPrefs.GetInt("Quality").ToString() + ", Current Volume: " + PlayerPrefs.GetFloat("Master Volume").ToString() + ".");

        switch (PlayerPrefs.GetInt("Days Beaten"))
        {
            case 0:
                day2.enabled = false;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day3.enabled = false;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day4.enabled = false;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day5.enabled = false;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day6.enabled = false;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                break;
            case 1:
                day2.enabled = true;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Day 2";
                day3.enabled = false;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day4.enabled = false;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day5.enabled = false;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day6.enabled = false;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                break;
            case 2:
                day2.enabled = true;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Day 2";
                day3.enabled = true;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Day 3";
                day4.enabled = false;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day5.enabled = false;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day6.enabled = false;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                break;
            case 3:
                day2.enabled = true;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Day 2";
                day3.enabled = true;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Day 3";
                day4.enabled = true;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Day 4";
                day5.enabled = false;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day6.enabled = false;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                break;
            case 4:
                day2.enabled = true;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Day 2";
                day3.enabled = true;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Day 3";
                day4.enabled = true;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Day 4";
                day5.enabled = true;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Day 5";
                day6.enabled = false;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                break;
            case 5:
                day2.enabled = true;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Day 2";
                day3.enabled = true;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Day 3";
                day4.enabled = true;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Day 4";
                day5.enabled = true;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Day 5";
                day6.enabled = true;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Day 6";
                break;
            default:
                day2.enabled = false;
                day2.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day3.enabled = false;
                day3.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day4.enabled = false;
                day4.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day5.enabled = false;
                day5.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                day6.enabled = false;
                day6.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
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

        //Handle Anything Else Here.

        //Then hide everything that needs to be hidden depending on the transition.

        if (PlayerPrefs.GetInt("From Level") == 1)
        {
            mainPage.SetActive(false);
            settingsPage.SetActive(false);
            levelPage.SetActive(true);
            fade.SetActive(true);
            fadeIn = true;
            FindObjectOfType<AudioScript>().FadeIn();
        }
        else
        {
            mainPage.SetActive(true);
            settingsPage.SetActive(false);
            levelPage.SetActive(false);
            fadeAlpha.alpha = 0f;
            fade.SetActive(false);
            fadeIn = false;
        }

        PlayerPrefs.SetInt("From Level", 0);
    }

    // Update is called once per frame
    void Update()
    {
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
                SceneManager.LoadScene("Level 1");
            }
        }
    }

    public void Settings()
    {
        mainPage.SetActive(false);
        settingsPage.SetActive(true);
        levelPage.SetActive(false);
    }

    public void SelectLevel()
    {
        mainPage.SetActive(false);
        settingsPage.SetActive(false);
        levelPage.SetActive(true);
    }

    public void Exit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void LevelLoad(Button button)
    {
        //Load the Passed Level
        PlayerPrefs.SetString("Day Number", button.GetComponentInChildren<TextMeshProUGUI>().text);
        FindObjectOfType<AudioScript>().FadeOut();
        fade.SetActive(true);
        fadeIn = false;
    }

    public void VolumeSlider(Slider slider)
    {
        PlayerPrefs.SetFloat("Master Volume", slider.value);
        volumeText.text = slider.value.ToString();
        FindObjectOfType<AudioScript>().VolumeChange();
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

    public void SaveAndExit()
    {
        PlayerPrefs.Save();
        mainPage.SetActive(true);
        settingsPage.SetActive(false);
        levelPage.SetActive(false);
    }

    public void BackToMenu()
    {
        mainPage.SetActive(true);
        settingsPage.SetActive(false);
        levelPage.SetActive(false);
    }
    
    public void OnTouch()
    {
        FindObjectOfType<AudioScript>().Play("Click");
    }
}
