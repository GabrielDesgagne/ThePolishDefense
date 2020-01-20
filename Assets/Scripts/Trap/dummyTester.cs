using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dummyTester : MonoBehaviour
{
    
    private void FixedUpdate()
    {
        Vector3 dir = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += -Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        GetComponent<Rigidbody>().AddForce(dir * 15, ForceMode.Force);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, 1 * Time.fixedDeltaTime, 0));
        GetComponent<Rigidbody>().AddTorque(0, -10*Time.fixedDeltaTime,0);


    }
}
