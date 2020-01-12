using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningTrigger : MonoBehaviour
{

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger")
        {
            Debug.Log("Start game!");
        }
        //Debug.Log("TrigEnter");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("TrigStay");


    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("TrigExit");

        


    }
}
