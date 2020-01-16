using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Projectile {
    public Potion(GameObject ob)
    {
        Obj = ob;
        SlerpPct = 0;
        Type = ProjectileType.POTION;
    }
}
