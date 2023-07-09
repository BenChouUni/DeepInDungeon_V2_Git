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

    int actionIndex = 0;

    //可能用做AI的實現
    [SerializeField]
    public List<EnemyActionSet> actionList;

    public EnemyData(int _id,string _name, int _maxHp, int _shield,int _atk) : base(_name, _maxHp, _shield)
    {
        this.id = _id;
        this.atk = _atk;
        this.targetType = CharaterType.Enemy;
        actionIndex = 0;

    }

    public void DoAction()
    {
        //暫時
        //foreach (var item in actionList)
        //{
        //    item.DoAction(this);
        //}
        //輪流做
        actionList[actionIndex].DoAction(this);
        actionIndex++;
        if (actionIndex >= actionList.Count -1)
        {
            actionIndex = 0;
        }
    }
}
