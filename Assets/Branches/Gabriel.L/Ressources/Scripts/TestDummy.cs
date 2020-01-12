using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 dir = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += -Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }
        GetComponent<Rigidbody>().AddForce(dir * speed, ForceMode.Force);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, 1 * Time.fixedDeltaTime, 0));
        GetComponent<Rigidbody>().AddTorque(0, -10 * Time.fixedDeltaTime, 0);
    }
}
