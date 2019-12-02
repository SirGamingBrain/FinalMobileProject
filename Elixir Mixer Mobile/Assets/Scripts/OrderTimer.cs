using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderTimer : MonoBehaviour
{
    TextMeshProUGUI timer;
    float actualTime = 60f;
    float minutes = 0f;
    float seconds = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = this.GetComponentInChildren<TextMeshProUGUI>();
        minutes = Mathf.Floor(actualTime / 60);
        seconds = (actualTime % 60);
        timer.text = (minutes.ToString("0") + ":00");
    }

    // Update is called once per frame
    void Update()
    {
        if (actualTime > 0f)
        {
            actualTime -= Time.deltaTime;
            minutes = Mathf.Floor(actualTime / 60);
            seconds = (actualTime % 60);

            if (seconds == 0)
            {
                timer.text = (minutes.ToString("00") + ":00");
            }
            else
            {
                timer.text = (minutes.ToString("00") + ":" + seconds.ToString("00"));
            }

            
        }
        else
        {
            FindObjectOfType<GameController>().FailedOrder(2);
            Destroy(this.gameObject);
        }
        
    }
}
