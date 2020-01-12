using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Flow
{

    #region Singleton
    static private WaveManager instance = null;

    static public WaveManager Instance
    {
        get {
            return instance ?? (instance = new WaveManager());
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

    override public void EndFlow()
    {

    }

}
