using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAction {

    public delegate void ActionDelegate();

    private TimeManager timeManager;

    private float timeLeft;
    private ActionDelegate function;


    public TimedAction(ActionDelegate _function, float timer) {
        timeManager = TimeManager.Instance;

        this.function = _function;
        this.timeLeft = timer;
    }

    public void Refresh(float deltatime) {
        //Reduce timer
        DecreaseTimer(deltatime);
        //Check if function should be called
        if (this.timeLeft <= 0) {
            this.function.Invoke();
            if (timeManager != null)
                timeManager.RemoveTimedAction(this);
        }
    }

    private void DecreaseTimer(float deltatime) {
        this.timeLeft -= deltatime;
    }
}

public class TimeManager : Flow {
    #region Singleton
    private TimeManager() { }
    private static TimeManager instance;
    public static TimeManager Instance {
        get {
            return instance ?? (instance = new TimeManager());
        }
    }
    #endregion

    private HashSet<TimedAction> actions;
    private HashSet<TimedAction> toRemoveActions;

    public override void PreInitialize() {
        actions = new HashSet<TimedAction>();
        toRemoveActions = new HashSet<TimedAction>();
    }

    public override void Initialize() { }

    public override void Refresh() {

        float deltatime = Time.deltaTime;

        //Update all actions
        foreach (TimedAction action in this.actions) {
            action.Refresh(deltatime);
        }

        //Removed used actions
        CleanActionLists();
    }

    public override void PhysicsRefresh() { }

    public override void EndFlow() {
        instance = null;
    }

    public void AddTimedAction(TimedAction action) {
        this.actions.Add(action);
    }

    public void RemoveTimedAction(TimedAction action) {
        this.toRemoveActions.Add(action);
    }

    private void CleanActionLists() {
        foreach (TimedAction action in this.toRemoveActions) {
            this.actions.Remove(action);
        }

        this.toRemoveActions.Clear();
    }


}
