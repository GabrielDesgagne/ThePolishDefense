using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public int maxAngle=120;
    public bool openDoor=false;
    private int currentAngle;
    GameObject door;
    Transform pivot;
    Quaternion initialRotation;
    int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("Door");
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            if (currentAngle < maxAngle)
            {
                currentAngle += speed;
                door.transform.RotateAround(pivot.transform.position, Vector3.up, currentAngle*Time.deltaTime);
            }

        }
        else{
            if (currentAngle >speed)
            {
                currentAngle -= speed;
                door.transform.RotateAround(pivot.transform.position, -Vector3.up, currentAngle * Time.deltaTime);
            }

        }
        if (currentAngle < 0)
        {
            door.transform.rotation = initialRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            Enemy e = other.gameObject.GetComponent<Enemy>();
            openDoor = true;
            Animator enemiAnim = other.gameObject.GetComponent<Animator>();
            
            e.canEnter = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            openDoor = false;
        }

    }
}
