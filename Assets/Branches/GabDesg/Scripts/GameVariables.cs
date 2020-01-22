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
            InitPathTilesCoords();
            InitInactiveTiles();
        }
        else {
            Destroy(this.gameObject);
        }            
    }
    #endregion

    [Header("Grids")]
    [SerializeField] public ushort mapRows = 12;
    [SerializeField] public ushort mapColumns = 12;

    [HideInInspector] public GameObject gridsHolder;
    [HideInInspector] public List<Vector2> pathTilesCoords;
    [HideInInspector] public List<Vector2> inactiveTilesCoords;


    [Header("Player")]
    [SerializeField] public Player playerPrefab;


    private void InitPathTilesCoords() {
        instance.pathTilesCoords.Add(new Vector2(11, 12));
        instance.pathTilesCoords.Add(new Vector2(11, 11));
        instance.pathTilesCoords.Add(new Vector2(11, 10));
        instance.pathTilesCoords.Add(new Vector2(11, 9));
        instance.pathTilesCoords.Add(new Vector2(11, 8));
        instance.pathTilesCoords.Add(new Vector2(11, 7));
        instance.pathTilesCoords.Add(new Vector2(11, 6));
        instance.pathTilesCoords.Add(new Vector2(11, 5));

        instance.pathTilesCoords.Add(new Vector2(10, 5));
        instance.pathTilesCoords.Add(new Vector2(9, 5));
        instance.pathTilesCoords.Add(new Vector2(8, 5));
        instance.pathTilesCoords.Add(new Vector2(7, 5));

        instance.pathTilesCoords.Add(new Vector2(7, 6));
        instance.pathTilesCoords.Add(new Vector2(7, 7));
        instance.pathTilesCoords.Add(new Vector2(7, 8));
        instance.pathTilesCoords.Add(new Vector2(7, 9));
        instance.pathTilesCoords.Add(new Vector2(7, 10));
        instance.pathTilesCoords.Add(new Vector2(7, 11));

        instance.pathTilesCoords.Add(new Vector2(6, 11));
        instance.pathTilesCoords.Add(new Vector2(5, 11));
        instance.pathTilesCoords.Add(new Vector2(4, 11));
        instance.pathTilesCoords.Add(new Vector2(3, 11));

        instance.pathTilesCoords.Add(new Vector2(3, 10));
        instance.pathTilesCoords.Add(new Vector2(3, 9));
        instance.pathTilesCoords.Add(new Vector2(3, 8));
        instance.pathTilesCoords.Add(new Vector2(3, 7));
        instance.pathTilesCoords.Add(new Vector2(3, 6));
        instance.pathTilesCoords.Add(new Vector2(3, 5));
        instance.pathTilesCoords.Add(new Vector2(3, 4));
        instance.pathTilesCoords.Add(new Vector2(3, 3));
        instance.pathTilesCoords.Add(new Vector2(3, 2));
        instance.pathTilesCoords.Add(new Vector2(3, 1));
        instance.pathTilesCoords.Add(new Vector2(3, 0));
    }

    private void InitInactiveTiles()
    {
        //TODO add inactive tiles
    }
}