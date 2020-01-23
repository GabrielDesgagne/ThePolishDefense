using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    SPIKE,
    MINE,
    GLUE
}
public class Trap {
    public TrapType Type { get; set; }
    public GameObject TrapObject { get; set; }

    public Trap(GameObject trap, TrapType type)
    {
        this.TrapObject = trap;
        this.Type = type;
    }
}
