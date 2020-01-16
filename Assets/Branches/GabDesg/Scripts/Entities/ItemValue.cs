using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemValue : MonoBehaviour{
    public TowerType? towerType { get; set; }
    public TrapName? trapType { get; set; }
    public bool itemWasPlacedOnMap { get; set; }
    public Vector2 positionOnMap { get; set; }

    ItemValue() {
        this.towerType = TowerType.BASIC;
        this.itemWasPlacedOnMap = false;
    }
}
