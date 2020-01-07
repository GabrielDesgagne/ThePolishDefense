using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float x;
    public Vector3 rot;
    float clampedAngles = 45; //was 70
    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        float d = 0;
        float sideways = 0;
        if (Input.GetKey(KeyCode.W))
            d = 20;
        else if (Input.GetKey(KeyCode.S))
            d = -20;

        if (Input.GetKey(KeyCode.A))
            sideways = 20;
        else if (Input.GetKey(KeyCode.D))
            sideways = -20;
        
        transform.Rotate(new Vector3(d*Time.deltaTime, 0, 0));
       // rot = transform.localRotation.eulerAngles;
       // var offset = 180f;
        x = transform.eulerAngles.x;
        rot = transform.eulerAngles;
        if (x < 180)
            x = Mathf.Clamp(x, 0, clampedAngles);
        else
            x = Mathf.Clamp(x, 360 - clampedAngles, 360);
       // x = Mathf.Clamp(x, offset-70, offset + 70);
       // if (x < 360)
       //     x = -360 + x; 
       //  rot.x = x;
        transform.eulerAngles = new Vector3(x,transform.eulerAngles.y + sideways*Time.deltaTime ,transform.eulerAngles.z);

    }
}
