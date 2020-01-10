using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : Flow
{
    #region Singleton
    static private TrapManager instance = null;

    static public TrapManager Instance
    {
        get {
            return instance ?? (instance = new TrapManager());
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
