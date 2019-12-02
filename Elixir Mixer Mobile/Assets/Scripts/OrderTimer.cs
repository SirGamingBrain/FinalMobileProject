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

    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        timer = this.GetComponentInChildren<TextMeshProUGUI>();
        minutes = Mathf.Floor(actualTime / 60);
        seconds = (actualTime % 60);
        timer.text = (minutes.ToString("0") + ":00");

        parent = this.transform.parent.gameObject;
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
            int pass = 0;

            switch (parent.name)
            {
                case "Holder 1":
                    pass = 0;
                    break;
                case "Holder 2":
                    pass = 1;
                    break;
                case "Holder 3":
                    pass = 2;
                    break;
                case "Holder 4":
                    pass = 3;
                    break;
                case "Holder 5":
                    pass = 4;
                    break;
                case "Holder 6":
                    pass = 5;
                    break;
                default:
                    pass = 0;
                    break;
            }

            FindObjectOfType<GameController>().FailedOrder(pass);
            Destroy(this.gameObject);
        }
        
    }
}
