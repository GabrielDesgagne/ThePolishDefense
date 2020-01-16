using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    Animator enemiAnim;
    private float openDoorTime = 0;
    float stateDuration=2f;
    Enemy e;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Porte").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("openDoor"))
        {
            openDoorTime += Time.deltaTime;
        }
        if (openDoorTime > stateDuration)
        {
            openDoorTime = 0;
            e.speed = e.startSpeed;
            enemiAnim.SetBool("isWalk", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            anim.SetBool("openDoor", true);
            enemiAnim = other.gameObject.GetComponent<Animator>();
            e=other.gameObject.GetComponent<Enemy>();
            enemiAnim.SetTrigger("idle");
            e.speed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            anim.SetBool("openDoor", false);
    }
}
