using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Projectile {
    public Potion(GameObject ob)
    {
        Obj = ob;
        SlerpPct = 0;
        Type = ProjectileType.POTION;
        Damage = TowerLink.tl.potionDamage;
        Radius = TowerLink.tl.potionRadius;
    }

    public override void CollisionHit()
    {
        EnemyManager.Instance.DamageEnemiesInRange(Obj.transform.position, Radius, (int)Damage);
        ProjectileManager.Instance.sparkParticle.gameObject.transform.position = Obj.transform.position;
        ProjectileManager.Instance.sparkParticle.Play();
    }
}
