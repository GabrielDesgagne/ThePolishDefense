using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower {
    private List<Potion> throwablePotions = new List<Potion>();

    public IceTower()
    {
        this.Position = new Vector3(0, 0, 0);
        this.Type = TowerType.ICE;
        this.IsPlayerActive = false;
        this.Range = 50;
        this.Damage = 3;
        this.DefaultAttackCooldown = 3;
    }

    public IceTower(Vector3 position, float damage, float range, float attackCooldown)
    {
        this.Position = position;
        this.Type = TowerType.ICE;
        this.IsPlayerActive = false;
        this.Range = range;
        this.Damage = damage;
        this.DefaultAttackCooldown = attackCooldown;
    }

    Transform positionPos = null;
    public override void Initialize()
    {
        this.Obj = GameObject.Instantiate(TowerManager.Instance.prefabs[Type], Position, Quaternion.identity);
        this.Info = Obj.GetComponent<TowerInfo>();
        GameObject potionPrefab = ProjectileManager.Instance.projectilePrefab[ProjectileType.POTION];
        Transform[] tfList = Obj.GetComponentsInChildren<Transform>();
        foreach (Transform tf in tfList)
        {
            if (tf.name == "PotionPos")
                positionPos = tf;
        }
        //method should be called when teleporting to tower that has towerType.ICE
        ProjectileManager.Instance.MoveThrowablePotions(Position + new Vector3(0, positionPos.position.y, 0));
    }

    public override void PhysicsRefresh()
    {

    }

    public override void PreInitialize()
    {

    }

    public override void Refresh()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 startPos = Position + new Vector3(0, positionPos.position.y, 0);
            if (AutoShoot && ProjectileManager.Instance.IsReadyToShoot(ProjectileType.POTION))
            {
                ProjectileManager.Instance.BasicShoot(ProjectileType.POTION, startPos, TowerManager.Instance.GetTarget());
            }
            else if (!AutoShoot)
            {
                ProjectileManager.Instance.BasicShoot(ProjectileType.POTION, startPos, TowerManager.Instance.GetTarget());
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ProjectileManager.Instance.enabledProjectiles[ProjectileType.THROWABLE_POTION].Count > 0)
            {
                ProjectileManager.Instance.PickupThrowablePotion();
                TimeManager.Instance.AddTimedAction(new TimedAction(() =>
                {
                    ProjectileManager.Instance.SpawnThrowablePotion(Position + new Vector3(0, positionPos.position.y, 0));
                }, 5));
            }
        }

        ChangeTowerStats();
    }
}
