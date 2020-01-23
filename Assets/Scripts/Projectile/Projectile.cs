using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile {
    public GameObject Obj { get; set; }
    public ProjectileType Type { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 TargetPos { get; set; }
    public Enemy Enemy { get; set; }
    public float SlerpPct { get; set; }
    public float Damage { get; set; }
    public float Radius { get; set; }
    public bool IsEnemyTarget { get; set; }

    public void MoveToTarget()
    {
        if (SlerpPct < 1)
        {
            if (!IsEnemyTarget)
            {
                Obj.transform.position = Vector3.Slerp(StartPos, TargetPos, SlerpPct);
            }
            else
            {
                if (Enemy != null)
                {
                    Obj.transform.position = Vector3.Slerp(StartPos, Enemy.transform.position, SlerpPct);
                }
                else if (Enemy == null)
                {
                    ProjectileManager.Instance.ResetProjectile(this);
                }
            }
            SlerpPct += 0.05f;
        }
    }

    public void Reset()
    {
        Obj.SetActive(false);
        Obj.transform.position = StartPos;
        SlerpPct = 0;
    }

    public abstract void CollisionHit();
}
