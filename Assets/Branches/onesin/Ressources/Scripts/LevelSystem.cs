using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level System", menuName = "Level System")]
public class LevelSystem : ScriptableObject
{
    public Level[] levels;
    public NUM_Level currentLevel;
}

public enum NUM_Level
{
    LEVEL_1, LEVEL_2, LEVEL_3
}

[System.Serializable]

public class Level
{
    public NUM_Level numLevel;
    public Wave[] waves;
    public float timeBetweenWaves;
}