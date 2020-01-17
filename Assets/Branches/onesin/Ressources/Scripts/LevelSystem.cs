using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level System")]
public class LevelSystem : ScriptableObject
{
    Wave[] waves;
    float timeBetweenWaves;
    int currentLevel;

}
