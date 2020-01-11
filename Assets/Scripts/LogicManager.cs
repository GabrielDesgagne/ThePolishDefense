using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : Flow
{
    #region Singleton
    static private LogicManager instance = null;

    static public LogicManager Instance
    {
        get {
            return instance ?? (instance = new LogicManager());
        }
    }

    #endregion

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {

    }

    override public void Refresh()
    {

    }

    override public void PhysicsRefresh()
    {

    }
}
