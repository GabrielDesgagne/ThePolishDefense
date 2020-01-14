using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Wave",menuName ="Wave")]
public class Wav : ScriptableObject
{
    public GameObject enemy;
    public int count;
    public float rate;
}
