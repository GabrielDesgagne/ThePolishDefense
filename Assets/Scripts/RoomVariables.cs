using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariables : MonoBehaviour
{
    #region Singleton
    public static RoomVariables instance;
    private void Awake() {

        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
    #endregion



    [Header("Grids")]
    [SerializeField] public ushort shopTurretRows = 2;
    [SerializeField] public ushort shopTurretColumns = 2;
    [SerializeField] public ushort shopTrapRows = 2;
    [SerializeField] public ushort shopTrapColumns = 2;

    [SerializeField] public Transform mapStartPointInShop;
    [SerializeField] public Transform shopTurretStartPoint;
    [SerializeField] public Transform shopTrapStartPoint;


    [Header("Player")]
    [SerializeField] public Player playerInstance;
}
