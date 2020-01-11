using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower {
    public BombFeeder Feeder { get; private set; }

    public HeavyTower() {
        this.Position = new Vector3(0, 0, 0);
        this.Type = TowerType.HEAVY;
        this.IsPlayerActive = false;
        this.Range = 50;
        this.DefaultAttackCooldown = 3;
    }

    public HeavyTower(Vector3 position, float damage, float range, float attackCooldown) {
        this.Position = position;
        this.Type = TowerType.HEAVY;
        this.IsPlayerActive = false;
        this.Range = range;
        this.Damage = damage;
        this.DefaultAttackCooldown = attackCooldown;
    }

    public override void Initialize() {
        this.Object = GameObject.Instantiate(TowerManager.Instance.prefabs[Type], Position, Quaternion.identity);
        Feeder = new BombFeeder(Position + new Vector3(0, 43.75f, 0));
        Feeder.SpawnBombs();
    }

    public override void PhysicsRefresh() {
        
    }

    public override void PreInitialize() {

    }

    public override void Refresh() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Feeder.Pickup();
        }
        Feeder.Move();
    }
}
