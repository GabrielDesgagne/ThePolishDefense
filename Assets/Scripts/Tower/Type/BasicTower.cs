using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower {
    public BasicTower()
    {
        this.Position = new Vector3(0, 0, 0);
        this.Type = TowerType.BASIC;
        this.IsPlayerActive = false;
        this.Range = 50;
        this.Damage = 3;
        this.DefaultAttackCooldown = 3;
    }

    public BasicTower(Vector3 position, float damage, float range, float attackCooldown)
    {
        this.Position = position;
        this.Type = TowerType.BASIC;
        this.IsPlayerActive = false;
        this.Range = range;
        this.Damage = damage;
        this.DefaultAttackCooldown = attackCooldown;
    }

    public override void Initialize()
    {
        this.Obj = GameObject.Instantiate(TowerManager.Instance.prefabs[TowerType.BASIC], Position, Quaternion.identity, TowerManager.Instance.towerParent[Type].transform);
    }

    public override void PhysicsRefresh()
    {

    }

    public override void PreInitialize()
    {

    }

    public override void Refresh()
    {

    }
}
