using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Flow
{
    #region Singleton
    static private PlayerManager instance = null;

    static public PlayerManager Instance
    {
        get {
            return instance ?? (instance = new PlayerManager());
        }
    }

    #endregion

    override public void PreInitialize()
    {
        //TODO PLayer...
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

    override public void EndFlow()
    {

    }

}
