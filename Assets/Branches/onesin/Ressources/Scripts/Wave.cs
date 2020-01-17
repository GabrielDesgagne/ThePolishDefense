using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    [HideInInspector]
    public List<GameObject> enemy;
    public EnemyTypeByWave[] types;
    //public int count;
    public float rate;

    public void Load()
    {
        enemy = new List<GameObject>();
        for (int i = 0; i < types.Length; i++)
        {
            for (int j = 0; j < types[i].number; j++)
            {
                enemy.Add(Resources.Load<GameObject>("Prefabs/Enemy/" + types[i].type.ToString()));
            }
        }
    }
}

[System.Serializable]
public class EnemyTypeByWave
{
    public EnemyType type;
    public int number;
}
