using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfColliderTrigger : MonoBehaviour
{
    public bool collide = true;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        if (!gameObject.activeSelf)
        {
            collide = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collide = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collide = false;
    }
}
