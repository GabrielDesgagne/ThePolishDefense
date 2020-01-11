using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower {
    public BombFeeder Feeder { get; private set; }
    public bool AutoShoot { get; set; }//if true, no wait timer, if false, has to wait until the feeder is ready
    private List<Bomb> enabledBombs = new List<Bomb>();
    private List<Bomb> disabledBombs = new List<Bomb>();


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
        for (int i = 0; i < 10; i++) {
            Bomb bomb = new Bomb(GameObject.Instantiate(Feeder.BombList[0]));
            disabledBombs.Add(bomb);
            bomb.Object.SetActive(false);
        }
        AutoShoot = true;//will be set through upgrades or something like that
    }

    public override void PhysicsRefresh() {

    }

    public override void PreInitialize() {

    }

    public override void Refresh() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (AutoShoot && disabledBombs.Count > 0) {
                BasicShoot();
            } else if (!AutoShoot && Feeder.IsReady) {
                BasicShoot();
            }
        }
        Feeder.Move();
        Vector3 target = new Vector3(100, 10, 0);//test vector, will later get closest enemy
        BombMoveToTarget(target);
    }

    public void BasicShoot() {
        Feeder.Pickup();
        enabledBombs.Add(disabledBombs[0]);
        enabledBombs[enabledBombs.Count - 1].Object.SetActive(true);
        disabledBombs.RemoveAt(0);
    }

    public void BombMoveToTarget(Vector3 target) {
        for (int i = 0; i < enabledBombs.Count; i++) {
            if (enabledBombs[i].SlerpPct < 1) {

                enabledBombs[i].Object.transform.position = Vector3.Slerp(Feeder.Position + new Vector3(0, Feeder.topY, 0), target, enabledBombs[i].SlerpPct);
                enabledBombs[i].SlerpPct += 0.01f;
            } else {
                enabledBombs[i].Object.SetActive(false);
                enabledBombs[i].Object.transform.position = Feeder.Position;
                enabledBombs[i].SlerpPct = 0;
                disabledBombs.Add(enabledBombs[i]);
                enabledBombs.Remove(enabledBombs[i]);
            }
        }
    }
}
