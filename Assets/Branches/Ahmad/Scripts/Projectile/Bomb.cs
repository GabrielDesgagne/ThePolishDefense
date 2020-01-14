using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bomb : Projectile {
    public Bomb(GameObject ob) {
        Object = ob;
        SlerpPct = 0;
    }
}
