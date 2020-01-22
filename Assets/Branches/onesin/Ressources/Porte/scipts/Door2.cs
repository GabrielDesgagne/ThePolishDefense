using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public int maxAngle=90;
    public bool openDoor=false;
    private int currentAngle=0;
    public GameObject doorLeft;
    public GameObject doorRight;
    public Transform pivotLeft;
    public Transform pivotRight;
    Quaternion initialRotation;
    int speed = 1;
    public float Hp=5;
    // Start is called before the first frame update
    void Start()
    {
        //door = GameObject.Find("LeftDoor");
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
                doorLeft.transform.RotateAround(pivotLeft.transform.position, Vector3.up, -currentAngle*Time.deltaTime);
                doorRight.transform.RotateAround(pivotRight.transform.position, Vector3.up, currentAngle * Time.deltaTime);
            }

        }
        /*else{
            if (currentAngle >speed)
            {
                currentAngle -= speed;
                doorLeft.transform.RotateAround(pivotLeft.transform.position, -Vector3.up, -currentAngle * Time.deltaTime);
                doorRight.transform.RotateAround(pivotRight.transform.position, -Vector3.up, currentAngle * Time.deltaTime);
            }

        }
        if (currentAngle <= 0)
        {
            doorLeft.transform.rotation = initialRotation;
            doorRight.transform.rotation = initialRotation;
        }*/
        //openDoor = currentAngle > maxAngle/4 ;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            Enemy e = other.gameObject.GetComponent<Enemy>();

            //Animator enemiAnim = other.gameObject.GetComponent<Animator>();
            
            //e.canAttack = true;
            //e.door = this;
            e.stateDuration = e.MaxStateDuration * (1 - (currentAngle / maxAngle));
            if (!openDoor)
            {
                e.Kill();
            }
            /*else
            {
                e.canEnter = true;
            }*/
            openDoor = true;
            
        }
    }

    /*public void takeDamage(float damage)
    {
        Hp -= damage;
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //openDoor = false;
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.canEnter = false;
        }

    }
}
