using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableManager : Flow
{
    #region Singleton
    static private GrabbableManager instance = null;

    static public GrabbableManager Instance
    {
        get {
            return instance ?? (instance = new GrabbableManager());
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
