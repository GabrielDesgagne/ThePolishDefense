using System;
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
    [SerializeField] public ushort mapGridWidth;
    [SerializeField] public ushort mapGridHeight;
    [SerializeField] public ushort shopGridWidth;
    [SerializeField] public ushort shopGridHeight;

    [SerializeField] public Transform shopRoomPosition;
    [SerializeField] public Transform mapRoomPosition;

    [Header("Shop Prefabs")]
    [SerializeField] public GameObject turretBasicPrefab;

}