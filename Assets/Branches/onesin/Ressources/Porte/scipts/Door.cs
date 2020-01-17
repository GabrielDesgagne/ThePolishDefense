using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
       
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Porte").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.canEnter = true;
            anim.SetBool("openDoor", true);

            //Animator enemiAnim = other.gameObject.GetComponent<Animator>();

            //enemiAnim.SetTrigger("idle");
            //e.speed = 0;
            //e.isHittable = false;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            anim.SetBool("openDoor", false);

        }
            
    }
}
