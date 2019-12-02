using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    float levelTimer = 300;

    int minimumScore = 50;

    bool timerOn = false;
    bool dayFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        //Check and see which day we are playing.

        //Based on the day selected, need to hide certain objects, introduce new features, and explain any other changes.

        //Once the intro is complete, the day will begin.
    }

    // Update is called once per frame
    void Update()
    {
        //Timer runs until the day is over, at which point we need an end screen.
        if (timerOn)
        {
            levelTimer -= Time.deltaTime;

            //Update the clock hand.

            if (levelTimer<= 0f)
            {
                dayFinished = true;
            }
        }

        if (dayFinished)
        {
            //Call a function to display the finishing stats to the player.
            //This will provide the player with knowing if they met the goal or not, and access to letting them retry if they failed.
        }
    }
}
