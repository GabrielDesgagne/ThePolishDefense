using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile {
    public Bomb(GameObject ob)
    {
        Obj = ob;
        SlerpPct = 0;
        Type = ProjectileType.BOMB;
        Damage = TowerLink.tl.bombDamage;
        Radius = TowerLink.tl.bombRadius;
    }

    public override void CollisionHit()
    {
        EnemyManager.Instance.DamageEnemiesInRange(Obj.transform.position, Radius, (int)Damage);
        ProjectileManager.Instance.explosionParticle.gameObject.transform.position = Obj.transform.position;
        ProjectileManager.Instance.explosionParticle.Play();
    }
}
