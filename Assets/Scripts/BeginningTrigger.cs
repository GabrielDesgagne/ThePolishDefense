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
        
            Debug.Log("Check if Handle has been grabbed.");
        
        //Debug.Log("TrigEnter");
    }

    private void OnTriggerStay(Collider other)
    {
//        Debug.Log("If (grabbed)  initiate launch game event, and render table NON kinematic");

        //Debug.Log("TrigStay");


    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("TrigExit");

        


    }
}
