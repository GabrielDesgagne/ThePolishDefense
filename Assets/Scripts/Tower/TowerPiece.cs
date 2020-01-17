using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPiece : MonoBehaviour, IGrabbable {

    public TowerType currentType;
    [HideInInspector]
    public bool itemWasPlacedOnMap;
    public Vector2 positionOnMap;

    public void Dropped() {
        ShopManager.Instance.ListenForItemDropped(gameObject, this);
    }

    public void Grabbed() {
        ShopManager.Instance.ListenForItemPickedUp(gameObject, this);
    }

    public void Highlight() {

    }

    public void UnHighlight() {

    }

    private void Update() {
        switch (currentType) {
            case TowerType.BASIC:
                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.Q)) {
                    Dropped();
                }
                break;
            case TowerType.HEAVY:
                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.W)) {
                    Dropped();
                }
                break;
            case TowerType.ICE:
                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.E)) {
                    Dropped();
                }
                break;
        }
    }
}