using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameFlow : MonoBehaviour {

    void Start() {
        GridManager.Instance.Initialize();
    }

    void Update() {
        GridManager.Instance.Refresh();        
    }

    void FixedUpdate() {
        GridManager.Instance.PhysicsRefresh();
    }
}
