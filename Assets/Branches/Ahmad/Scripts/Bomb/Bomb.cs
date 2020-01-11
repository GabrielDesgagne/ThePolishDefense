using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb {
    public GameObject Object { get; set; }
    public float SlerpPct { get; set; }

    public Bomb(GameObject ob) {
        Object = ob;
        SlerpPct = 0;
    }
}
