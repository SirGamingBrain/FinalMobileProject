using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I'm doing something!");
        FindObjectOfType<AudioScript>().Play("Male 01");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
