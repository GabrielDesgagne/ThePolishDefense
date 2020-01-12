using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

    [SerializeField] private bool showGrid = true;

    void Start() {
        if (showGrid)
            GridManager.Instance.Initialise();
    }

    void Update() {
        if (showGrid)
            GridManager.Instance.Refresh();
    }

    void FixedUpdate() {
        if (showGrid)
            GridManager.Instance.PhysicRefresh();
    }
}
