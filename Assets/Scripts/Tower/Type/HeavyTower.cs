﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower
{
    public Cannon Cannon { get; private set; }
    public BombFeeder Feeder { get; private set; }

    public HeavyTower()
    {
        this.Position = Vector3.zero;
        this.Type = TowerType.HEAVY;
        this.IsPlayerActive = false;
        this.IsReady = true;
        this.Range = 50;
        this.DefaultAttackCooldown = 3;
    }

    public HeavyTower(Vector3 position, float damage, float range, float attackCooldown)
    {
        this.Position = position;
        this.Type = TowerType.HEAVY;
        this.IsPlayerActive = false;
        this.IsReady = true;
        this.Range = range;
        this.Damage = damage;
        this.DefaultAttackCooldown = attackCooldown;
    }
    public override void Initialize()
    {
        this.Obj = GameObject.Instantiate(TowerManager.Instance.prefabs[Type], Position, Quaternion.identity, TowerManager.Instance.towerParent[Type].transform);
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
        Feeder = new BombFeeder(this, feederPos.position);
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
            if (Cannon.Angle < (Cannon.GetAngleToTarget(position) + 2) && Cannon.Angle > (Cannon.GetAngleToTarget(position) - 2))
            {
                ShootAtEnemy(enemy, Cannon.Obj.transform.position, ProjectileType.BOMB);
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
