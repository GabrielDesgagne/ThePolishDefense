using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPiece : MonoBehaviour, IGrabbable {

    public TrapType currentType;
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
            case TrapType.GLUE:
                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.R)) {
                    Dropped();
                }
                break;
            case TrapType.MINE:
                if (Input.GetKeyDown(KeyCode.Alpha5)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.T)) {
                    Dropped();
                }
                break;
            case TrapType.SPIKE:
                if (Input.GetKeyDown(KeyCode.Alpha6)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.Y)) {
                    Dropped();
                }
                break;
        }
    }
}
