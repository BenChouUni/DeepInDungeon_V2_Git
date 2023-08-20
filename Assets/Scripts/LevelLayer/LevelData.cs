using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int Layer;
    public int number;
    public EnemySO enemy;

    public LevelData(int _Layer, int _number, EnemySO _enemy)
    {
        this.Layer = _Layer;
        this.number = _number;
        this.enemy = _enemy;
    }
}
