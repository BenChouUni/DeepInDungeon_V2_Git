using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int Layer;
    public int number;
    public EnemyGroupSO EnemyGroup;

    public LevelData(int _Layer, int _number, EnemyGroupSO _enemy)
    {
        this.Layer = _Layer;
        this.number = _number;
        this.EnemyGroup = _enemy;
    }
}
