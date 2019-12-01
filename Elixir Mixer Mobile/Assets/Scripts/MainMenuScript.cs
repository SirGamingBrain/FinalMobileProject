using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    GameObject mainPage;
    GameObject settingsPage;
    GameObject levelPage;



    // Start is called before the first frame update
    void Start()
    {
        mainPage = GameObject.Find("Main Panel");
        settingsPage = GameObject.Find("Settings");
        levelPage = GameObject.Find("Level Select");

        mainPage.SetActive(true);
        settingsPage.SetActive(false);
        levelPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Settings()
    {
        mainPage.SetActive(false);
        settingsPage.SetActive(true);
        levelPage.SetActive(false);
    }

    void SelectLevel()
    {
        mainPage.SetActive(false);
        settingsPage.SetActive(false);
        levelPage.SetActive(true);
    }

    void Exit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    void Tutorial()
    {
        //Load the Tutorial
    }

    void LevelLoad()
    {
        //Load the Passed Level
    }

    void VolumeSlider()
    {
        //Change the Volume (This might have to be moved to the audio manager script)
    }

    void ToggleHigh()
    {
        //Set High Quality
    }

    void ToggleMedium()
    {
        //Set Medium Quality
    }

    void ToggleLow()
    {
        //Set Low Quality
    }

    void SaveAndExit()
    {
        //Save Settings and Open Main Menu
        mainPage.SetActive(true);
        settingsPage.SetActive(false);
        levelPage.SetActive(false);
    }

    void BackToMenu()
    {
        mainPage.SetActive(true);
        settingsPage.SetActive(false);
        levelPage.SetActive(false);
    }
}
