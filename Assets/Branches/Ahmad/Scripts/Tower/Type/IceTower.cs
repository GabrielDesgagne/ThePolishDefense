using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower {
    private List<Potion> throwablePotions = new List<Potion>();

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
        this.Object = GameObject.Instantiate(TowerManager.Instance.prefabs[Type], Position, Quaternion.identity);
        GameObject potionPrefab = ProjectileManager.Instance.projectilePrefab[ProjectileType.POTION];
        for (int i = 0; i < 10; i++) {
            Vector3 potionPos = Position + new Vector3(Random.Range(-1.5f, 1.5f), 47.61f, Random.Range(-1.5f, 1.5f));
            Potion throawablePotion = new Potion(GameObject.Instantiate(potionPrefab, potionPos, potionPrefab.transform.rotation));
            throwablePotions.Add(throawablePotion);
        }
    }

    public override void PhysicsRefresh() {

    }

    public override void PreInitialize() {

    }

    private Vector3 GetTarget() {
        return new Vector3(100, 10, -50);//test vector, will later get closest enemy
    }

    public override void Refresh() {
        Vector3 target = new Vector3(100, 10, -50);//test vector, will later get closest enemy
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 startPos = Position + new Vector3(Random.Range(-1.5f, 1.5f), 47.61f);
            if (AutoShoot && ProjectileManager.Instance.IsReadyToShoot(ProjectileType.POTION)) {
                ProjectileManager.Instance.BasicShoot(ProjectileType.POTION, startPos, GetTarget());
            } else if (!AutoShoot) {
                ProjectileManager.Instance.BasicShoot(ProjectileType.POTION, startPos, GetTarget());
            }
        }
    }
}
