using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData : Character
{
    [SerializeField]
    private int id;
    public int ID
    {
        get { return id; }
    }
    [SerializeField]
    private int atk;
    public int ATK
    {
        get { return atk; }
    }

    
    //可能用做AI的實現
    public List<CardActionBase> actionList;

    public EnemyData(int _id,string _name, int _maxHp, int _shield,int _atk) : base(_name, _maxHp, _shield)
    {
        this.id = _id;
        this.atk = _atk;
        this.targetType = TargetType.Enemy;
    }
}
