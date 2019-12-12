using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStatus : MonoBehaviour
{
    AudioSource human;
    float timer = 90f;

    public bool orderComplete;

    // Start is called before the first frame update
    void Start()
    {
        human = this.GetComponent<AudioSource>();
        Debug.Log("I'm doing something!");

        switch (this.name)
        {
            case "Male 01(Clone)":
                FindObjectOfType<GameAudioScript>().Play("Male 01", human);
                break;
            case "Male 02(Clone)":
                FindObjectOfType<GameAudioScript>().Play("Male 02", human);
                break;
            case "Female 01(Clone)":
                FindObjectOfType<GameAudioScript>().Play("Female 01", human);
                break;
            case "Female 02(Clone)":
                FindObjectOfType<GameAudioScript>().Play("Female 02", human);
                break;
            case "Warrior 01(Clone)":
                FindObjectOfType<GameAudioScript>().Play("Warrior 01", human);
                break;
            default:
                FindObjectOfType<GameAudioScript>().Play("Male 01", human);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void destroyTime()
    {
        Destroy(this.gameObject);
    }
}
