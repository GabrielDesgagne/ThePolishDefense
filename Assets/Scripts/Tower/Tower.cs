using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower {
    public GameObject Obj { get; protected set; }
    public Vector3 Position { get; protected set; }
    public GameObject TpButtonPos { get; protected set; }
    public TowerType Type { get; protected set; }
    public bool IsPlayerActive { get; protected set; }
    public bool AutoShoot { get; protected set; }
    public float Range { get; protected set; }
    public float Damage { get; protected set; }
    public float DefaultAttackCooldown { get; protected set; }
    public TowerInfo Info { get; protected set; }
    public bool IsReady { get; protected set; }

    public abstract void PreInitialize();
    public abstract void Initialize();
    public abstract void Refresh();
    public abstract void PhysicsRefresh();

    protected void ChangeTowerStats()
    {
        Damage = Info.TowerDamage;
        Range = Info.TowerRange;
        DefaultAttackCooldown = Info.TowerAttackCooldown;
    }

    protected void ShootAtEnemy(Enemy enemy, Vector3 startPos, ProjectileType type)
    {
        if (enemy != null)
        {
            ProjectileManager.Instance.BasicShoot(type, startPos, enemy);
            IsReady = false;
            TimeManager.Instance.AddTimedAction(new TimedAction(() =>
            {
                IsReady = true;
            }, DefaultAttackCooldown));
        }
    }
}
