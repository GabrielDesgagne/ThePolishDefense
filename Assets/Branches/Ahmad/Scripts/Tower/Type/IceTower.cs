using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower {
    public IceTower() {
        this.Position = new Vector3(0, 0, 0);
        this.Type = TowerType.ICE;
        this.IsPlayerActive = false;
        this.Range = 50;
        this.Damage = 3;
        this.DefaultAttackCooldown = 3;
    }

    public IceTower(Vector3 position, float damage, float range, float attackCooldown) {
        this.Position = position;
        this.Type = TowerType.ICE;
        this.IsPlayerActive = false;
        this.Range = range;
        this.Damage = damage;
        this.DefaultAttackCooldown = attackCooldown;
    }

    public override void Initialize() {

    }

    public override void PhysicsRefresh() {

    }

    public override void PreInitialize() {

    }

    public override void Refresh() {

    }
}
