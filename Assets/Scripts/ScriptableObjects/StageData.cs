using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class StageEnemy
{
    public EnemyData enemyData;
    public int count;
}

[CreateAssetMenu(fileName = "Stage_", menuName = "Stage Data")]
public class StageData : ScriptableObject
{
    public int stageNumber;
    public List<StageEnemy> enemies;

    [Header("Spawn Area")]
    public Vector3 spawnCenter;
    public Vector3 spawnSize;
}