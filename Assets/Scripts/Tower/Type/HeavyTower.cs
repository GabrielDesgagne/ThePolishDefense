using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower
{
    public Cannon Cannon { get; private set; }
    public BombFeeder Feeder { get; private set; }
    private bool isReady = true;

    public HeavyTower()
    {
        this.Position = Vector3.zero;
        this.Type = TowerType.HEAVY;
        this.IsPlayerActive = false;
        this.Range = 50;
        this.DefaultAttackCooldown = 3;
    }

    public HeavyTower(Vector3 position, float damage, float range, float attackCooldown)
    {
        this.Position = position;
        this.Type = TowerType.HEAVY;
        this.IsPlayerActive = false;
        this.Range = range;
        this.Damage = damage;
        this.DefaultAttackCooldown = attackCooldown;
    }
    public override void Initialize()
    {
        this.Obj = GameObject.Instantiate(TowerManager.Instance.prefabs[Type], Position, Quaternion.identity);
        this.Info = Obj.GetComponent<TowerInfo>();
        Transform feederPos = null;
        Transform cannonPos = null;
        Transform[] tfList = Obj.GetComponentsInChildren<Transform>();
        foreach (Transform tf in tfList)
        {
            if (tf.name == "FeederPos")
                feederPos = tf;
            else if (tf.name == "CannonPos")
                cannonPos = tf;
        }
        Feeder = new BombFeeder(feederPos.position);
        Cannon = new Cannon(this, cannonPos.position);
        Feeder.SpawnBombs();
        AutoShoot = true;//will be set through upgrades or something like that
    }

    public override void PhysicsRefresh()
    {
        Enemy enemy;
        if ((enemy = EnemyManager.Instance.FindFirstTargetInRange(Position, Range)) != null)
        {
            Vector3 position = enemy.transform.position;
            Cannon.Move(Position);
            Cannon.AngleMoveToTarget(position);
            Vector3 startPos = Feeder.Position + new Vector3(0, Feeder.topY, 0);
            if (Cannon.Angle < (Cannon.GetAngleToTarget(position) + 2) && Cannon.Angle > (Cannon.GetAngleToTarget(position) - 2))
            {
                if (enemy != null && isReady)
                {
                    ProjectileManager.Instance.BasicShoot(ProjectileType.BOMB, startPos, enemy);
                    isReady = false;
                    TimeManager.Instance.AddTimedAction(new TimedAction(() =>
                    {
                        isReady = true;
                    }, DefaultAttackCooldown));
                }
            }
        }
    }

    public override void PreInitialize()
    {

    }

    public override void Refresh()
    {
        Feeder.Move();
        Cannon.CannonInput();

        ChangeTowerStats();
    }
}
