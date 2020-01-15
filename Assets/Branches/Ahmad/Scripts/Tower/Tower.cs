using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower {
    public GameObject Object { get; protected set; }
    public Vector3 Position { get; protected set; }
    public GameObject TpButtonPos { get; protected set; }
    public TowerType Type { get; protected set; }
    public bool IsPlayerActive { get; protected set; }
    public bool AutoShoot { get; protected set; }
    public float Range { get; protected set; }
    public float Damage { get; protected set; }
    public float DefaultAttackCooldown { get; protected set; }

    public abstract void PreInitialize();
    public abstract void Initialize();
    public abstract void Refresh();
    public abstract void PhysicsRefresh();
}
