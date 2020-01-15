using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bomb : Projectile {
    public Bomb(GameObject ob)
    {
        Obj = ob;
        SlerpPct = 0;
        Type = ProjectileType.BOMB;
    }
}
