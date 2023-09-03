using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int Layer;
    public LevelType leveltype;
    public int number;
    public EnemyGroupData EnemyGroup;

    public LevelData(int _layer, int _number, LevelType _leveltype, EnemyGroupData _enemy)
    {
        this.Layer = _layer;
        this.leveltype = _leveltype;
        this.number = _number;
        this.EnemyGroup = _enemy;
    }
    public LevelData()
    {
        this.Layer = -2;
        this.leveltype = LevelType.NULL;
        this.number = -1;
        this.EnemyGroup = null;

    }
}
