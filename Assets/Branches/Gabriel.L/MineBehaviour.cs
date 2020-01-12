using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MineBehaviour : MonoBehaviour
{
    UnityAction action;
    public void SetEventTrigger(UnityAction a)
    {
        action = a;
    }
    public void SetEventAction(UnityAction a)
    {
        action = a;
    }
    public void SetEventExitTrigger(UnityAction a)
    {
        action = a;
    }
    public void SetEventRemove(UnityAction a)
    {
        action = a;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (action == null)
        {
            Debug.Log("Not event set with mine");
            return;
        }
        action.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if(action == null)
        {
            Debug.Log("no event when leaving the trigger");
            return;
        }
        action.Invoke();
    }
}
