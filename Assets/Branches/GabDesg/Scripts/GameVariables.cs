using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour {

    #region Singleton
    public static GameVariables instance;
    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [SerializeField] public GameObject randomPrefab;

    //Grid variables
    [Header("Grid Variables")]
    [SerializeField] public ushort hiddenGridWidth;
    [SerializeField] public ushort hiddenGridHeight;
    [SerializeField] public Grid hiddenGrid;
    [SerializeField] public GameObject gridsParent;
    [SerializeField] public GameObject gridContourPrefab;
    [SerializeField] public GameObject gridLaserHitBoxPrefab;
}
