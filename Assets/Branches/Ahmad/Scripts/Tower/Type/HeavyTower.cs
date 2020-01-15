using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower {
    public Cannon Canon { get; private set; }
    public BombFeeder Feeder { get; private set; }

    public HeavyTower() {
        this.Position = Vector3.zero;
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
        Canon = new Cannon(this, Position + new Vector3(0, 47.9f, 14));
        Feeder.SpawnBombs();
        AutoShoot = true;//will be set through upgrades or something like that
    }

    public override void PhysicsRefresh() {
        Canon.Move(Position);
    }

    public override void PreInitialize() {

    }

    private Vector3 GetTarget() {
        return new Vector3(100, 10, -50);//test vector, will later get closest enemy
    }

    public override void Refresh() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Feeder.Pickup();
            Vector3 startPos = Feeder.Position + new Vector3(0, Feeder.topY, 0);
            if (AutoShoot && ProjectileManager.Instance.IsReadyToShoot(ProjectileType.BOMB)) {
                ProjectileManager.Instance.BasicShoot(ProjectileType.BOMB, startPos, GetTarget());
            } else if (!AutoShoot && Feeder.IsReady) {
                ProjectileManager.Instance.BasicShoot(ProjectileType.BOMB, startPos, GetTarget());
            }
        }
        /*
            if (Input.GetKeyDown(KeyCode.Q)) {
            Feeder.Pickup();
            enabledBombs.Add(disabledBombs[0]);
            Canon.LoadCannon(enabledBombs[enabledBombs.Count - 1]);
            disabledBombs.RemoveAt(0);
        }*/
        Feeder.Move();
        Canon.CannonInput();
    }
}
