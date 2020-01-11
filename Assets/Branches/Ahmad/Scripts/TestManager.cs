using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {
    void Awake() {
        TowerManager.Instance.PreInitialize();
    }

    void Start() {
        TowerManager.Instance.Initialize();
    }

    void Update() {
        TowerManager.Instance.Refresh();
    }

    void FixedUpdate() {
        TowerManager.Instance.PhysicsRefresh();
    }
}
