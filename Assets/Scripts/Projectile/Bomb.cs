using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile {
    public Bomb(GameObject ob)
    {
        Obj = ob;
        SlerpPct = 0;
        Type = ProjectileType.BOMB;
        Damage = 3;
        Radius = 5;
    }

    public override void CollisionHit()
    {
        //EnemyManager.Instance.ProjectileCollisionHit(Obj.transform.position, Radius, Damage);
        ProjectileManager.Instance.explosionParticle.gameObject.transform.position = Obj.transform.position;
        ProjectileManager.Instance.explosionParticle.Play();

    }
}
