using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Flow
{
    #region Singleton
    static private BoardManager instance = null;

    static public BoardManager Instance
    {
        get {
            return instance ?? (instance = new BoardManager());
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
