using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower {
    public Cannon Canon { get; private set; }
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
        Canon = new Cannon(Position + new Vector3(0, 47.9f, 14));
        Feeder.SpawnBombs();
        for (int i = 0; i < 10; i++) {
            Bomb bomb = new Bomb(GameObject.Instantiate(Feeder.BombList[0]));
            disabledBombs.Add(bomb);
            bomb.Object.SetActive(false);
        }
        AutoShoot = true;//will be set through upgrades or something like that
    }

    public override void PhysicsRefresh() {
        Canon.Move(Position);
    }

    public override void PreInitialize() {

    }

    public override void Refresh() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 target = new Vector3(100, 10, 0);//test vector, will later get closest enemy
            if (AutoShoot && disabledBombs.Count > 0) {
                BasicShoot(target);
            } else if (!AutoShoot && Feeder.IsReady) {
                BasicShoot(target);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            Feeder.Pickup();
            enabledBombs.Add(disabledBombs[0]);
            Canon.LoadCannon(enabledBombs[enabledBombs.Count - 1]);
            disabledBombs.RemoveAt(0);
        }
        Feeder.Move();
        Canon.CannonInput();
        BombsMoveToTarget();
    }

    public void BasicShoot(Vector3 target) {
        Feeder.Pickup();
        enabledBombs.Add(disabledBombs[0]);
        enabledBombs[enabledBombs.Count - 1].StartPos = Feeder.Position + new Vector3(0, Feeder.topY, 0);
        enabledBombs[enabledBombs.Count - 1].TargetPos = target;
        enabledBombs[enabledBombs.Count - 1].Object.SetActive(true);
        disabledBombs.RemoveAt(0);
    }

    public void BombsMoveToTarget() {
        for (int i = 0; i < enabledBombs.Count; i++) {
            if (enabledBombs[i].SlerpPct < 1) {
                if (enabledBombs[i].Object.activeSelf) {
                    enabledBombs[i].BombMoveToTarget();
                }
            } else {
                ResetBomb(enabledBombs[i]);
            }
        }
    }

    public void ResetBomb(Bomb bomb) {
        bomb.Object.SetActive(false);
        bomb.Object.transform.position = Feeder.Position;
        bomb.SlerpPct = 0;
        disabledBombs.Add(bomb);
        enabledBombs.Remove(bomb);
    }
}
