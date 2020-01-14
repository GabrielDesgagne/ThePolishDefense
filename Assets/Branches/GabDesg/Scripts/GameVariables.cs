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
    [SerializeField] public ushort gridWidth;
    [SerializeField] public ushort gridHeight;
}
