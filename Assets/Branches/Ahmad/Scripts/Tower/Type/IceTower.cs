using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower {

    private List<Potion> enabledPotions = new List<Potion>();
    private List<Potion> disabledPotions = new List<Potion>();
    private List<Potion> throwablePotions = new List<Potion>();
    public bool AutoShoot { get; set; }

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
        GameObject potionPrefab = Resources.Load<GameObject>("Prefabs/Ice_Potion");
        for (int i = 0; i < 10; i++) {
            Vector3 potionPos = Position + new Vector3(Random.Range(-1.5f, 1.5f), 47.61f, Random.Range(-1.5f, 1.5f));
            Potion throawablePotion = new Potion(GameObject.Instantiate(potionPrefab, potionPos, potionPrefab.transform.rotation));
            throwablePotions.Add(throawablePotion);
            Potion potion = new Potion(GameObject.Instantiate(potionPrefab, new Vector3(0, 0, 0), potionPrefab.transform.rotation));
            disabledPotions.Add(potion);
        }
    }

    public override void PhysicsRefresh() {

    }

    public override void PreInitialize() {

    }

    public override void Refresh() {
        Vector3 target = new Vector3(100, 10, -50);//test vector, will later get closest enemy
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (AutoShoot && disabledPotions.Count > 0) {
                BasicShoot(target);
            } else if (!AutoShoot) {
                BasicShoot(target);
            }
        }

        if (Input.GetKeyDown(KeyCode.U)) {

        }

        PotionsMoveToTarget();
    }

    public void BasicShoot(Vector3 target) {
        enabledPotions.Add(disabledPotions[0]);
        enabledPotions[enabledPotions.Count - 1].StartPos = Position + new Vector3(Random.Range(-1.5f, 1.5f), 47.61f);
        enabledPotions[enabledPotions.Count - 1].TargetPos = target;
        enabledPotions[enabledPotions.Count - 1].Object.SetActive(true);
        disabledPotions.RemoveAt(0);
    }

    public void PotionsMoveToTarget() {
        for (int i = 0; i < enabledPotions.Count; i++) {
            if (enabledPotions[i].SlerpPct < 1) {
                if (enabledPotions[i].Object.activeSelf) {
                    enabledPotions[i].ProjectileMoveToTarget();
                }
            } else {
                ResetPotion(enabledPotions[i]);
            }
        }
    }

    public void ResetPotion(Potion potion) {
        potion.Object.SetActive(false);
        potion.Object.transform.position = Position + new Vector3(Random.Range(-1.5f, 1.5f), 47.61f);
        potion.SlerpPct = 0;
        disabledPotions.Add(potion);
        enabledPotions.Remove(potion);
    }
}
