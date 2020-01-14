using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MineBehaviour : MonoBehaviour
{
    UnityAction action;
    public GameObject minePrefab;

    public void onTrigger(UnityAction a)
    {
        action = a;
    }
    public void onAction(UnityAction a)
    {
        action = a;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (action == null)
        {
            Debug.Log("not action set");
            return;
        }
        action.Invoke();
    }
}
