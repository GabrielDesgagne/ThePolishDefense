using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile {
    public GameObject Obj { get; set; }
    public ProjectileType Type { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 TargetPos { get; set; }
    public float SlerpPct { get; set; }

    public void MoveToTarget()
    {
        if (SlerpPct < 1)
        {
            Obj.transform.position = Vector3.Slerp(StartPos, TargetPos, SlerpPct);
            SlerpPct += 0.01f;
        }
    }

    public void Reset()
    {
        Obj.SetActive(false);
        Obj.transform.position = StartPos;
        SlerpPct = 0;
    }
}
