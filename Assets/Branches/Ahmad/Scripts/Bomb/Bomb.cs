using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb {
    public Vector3 Position;

    public Bomb() {
        Position = new Vector3(0, 0, 0);
    }

    public Bomb(Vector3 position) {
        Position = position;
    }
}
