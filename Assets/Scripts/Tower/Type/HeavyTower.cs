using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTower : Tower {
    public Cannon Cannon { get; private set; }
    public BombFeeder Feeder { get; private set; }

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
        //this.Info = Obj.GetComponent<TowerInfo>(); Uncomment code when new prefab gets added and add script(TowerInfo) to the prefab
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
        Cannon.Move(Position);
    }

    public override void PreInitialize()
    {

    }

    public override void Refresh()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Feeder.Pickup();
            Vector3 startPos = Feeder.Position + new Vector3(0, Feeder.topY, 0);
            if (AutoShoot && ProjectileManager.Instance.IsReadyToShoot(ProjectileType.BOMB))
            {
                ProjectileManager.Instance.BasicShoot(ProjectileType.BOMB, startPos, TowerManager.Instance.GetTarget());
            }
            else if (!AutoShoot && Feeder.IsReady)
            {
                ProjectileManager.Instance.BasicShoot(ProjectileType.BOMB, startPos, TowerManager.Instance.GetTarget());
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
        Cannon.CannonInput();

        //ChangeTowerStats();//takes stats from editor || Uncomment code when new prefab gets added and add script(TowerInfo) to the prefab
    }
}
