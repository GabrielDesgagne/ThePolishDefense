using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Flow
{
    #region Singleton
    static private EnemyManager instance = null;

    static public EnemyManager Instance
    {
        get {
            return instance ?? (instance = new EnemyManager());
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
