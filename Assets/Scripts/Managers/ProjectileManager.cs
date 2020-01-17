using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Flow
{
    #region Singleton
    static private ProjectileManager instance = null;

    static public ProjectileManager Instance
    {
        get {
            return instance ?? (instance = new ProjectileManager());
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
