using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion {
    public GameObject Object { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 TargetPos { get; set; }
    public float SlerpPct { get; set; }

    public Potion(GameObject ob) {
        Object = ob;
        SlerpPct = 0;
    }

    public void ProjectileMoveToTarget() {
        if (SlerpPct < 1) {
            Object.transform.position = Vector3.Slerp(StartPos, TargetPos, SlerpPct);
            SlerpPct += 0.01f;
        }
    }
}
