using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoPck {

    #region Singleton
    private static MapInfoPck instance;
    public static MapInfoPck Instance {
        get {
            return instance ?? (instance = new MapInfoPck());
        }
    }
    #endregion

    //Variables

}
