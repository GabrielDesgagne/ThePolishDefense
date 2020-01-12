using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : Trap
{
    public bool inDetonate = false;
    public GameObject mineTest;
    MineBehaviour mineBehaviour;
    UnityAction action;
    Vector3 position;
    /* if (inDetonate)
         {
             if(currentTime <= 0)
             {

             }
             else
             {

         currentTime -= Time.deltaTime;
             }
         }*/
    public void Start()
    {
        //set timer
        currentTime = triggerTrap;

        //prefab = Resources.Load<GameObject>(MINE);
        mineTest = GameObject.Instantiate(prefab);
        mineBehaviour = mineTest.GetComponent<MineBehaviour>();

        //add action
        action += onTrigger;
        action += onAction;
        action += onExitTrigger;
        action += onRemove;

        //set action
        mineBehaviour.SetEventTrigger(action);
        mineBehaviour.SetEventAction(action);
        mineBehaviour.SetEventExitTrigger(action);
        mineBehaviour.SetEventRemove(action);
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // GameObject go = Instantiate(prefab, transform.position);
        }
        //CheckIfLoadedPrefabs();
        if (inDetonate)
        {
            currentTime -= Time.deltaTime;
        }
        if(currentTime == 0)
        {
            currentTime = 0;
            onAction();
        }
        //Debug.Log(action.GetInvocationList().Length);
    }
    void CheckIfLoadedPrefabs()
    {
        if (mineTest != null)
        {
            Debug.Log("is loaded : " + mineTest.name);
        }
        else
        {
            Debug.Log("not loaded");
        }
    }

    public override void onAction()
    {
        Debug.Log("this gonna activate effect on enemy");
        if(currentTime == 0)
        {
            Debug.Log("boom");
            //damage enemy
            inDetonate = false;
        }
        else
        {
            Debug.Log("wrong");
        }
    }

    public override void onExitTrigger()
    {
        Debug.Log("exit range of the trap gonna activate the trap or delete de trap");
    }

    public override void onRemove()
    {
        //only setactive to false when explosion goes of and timer to 0!!! else this aint gonna work
        Debug.Log("this been remove, removing the trap or the effect");
        this.gameObject.SetActive(false);
    }

    public override void onTrigger()
    {
        //Debug.Log("trigger : ");
        inDetonate = true;
        //add like click sound to avertir player the trap is activated ?




        /*if (currentTime == 0)
        {
            Debug.Log("boom");
            //damage enemy
            //remove trap
            inDetonate = false;
        }
        else
        {
            Debug.Log("wrong");
        }*/
    }
}
